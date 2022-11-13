using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp_Task
{
    /**
     * 
     * 要将同步解决方案转换为异步解决方案，
     * 最佳着手点在 GetURLContents 中，因为对 HttpWebRequest 方法 GetResponse 的调用以及对 Stream 方法 CopyTo 的调用是应用程序访问 Web 的位置。 
     * 
     * ValueTask<int> :Task 和 Task<TResult> 是引用类型，因此，性能关键路径中的内存分配会对性能产生负面影响，尤其当分配出现在紧凑循环中时。 支持通用返回类型意味着可返回轻量值类型（而不是引用类型），从而避免额外的内存分配。通过.Result属性获取结果
     * 
     * IAsyncEnumerable<string：可等待异步流。 异步流提供了一种方法，来枚举在具有重复异步调用的块中生成元素时从流中读取的项。
     * 
     * Void ：在异步事件处理程序中使用 void 返回类型，这需要 void 返回类型。 对于事件处理程序以外的不返回值的方法，应返回 Task，因为无法等待返回 void 的异步方法。 此类方法的任何调用方都必须继续完成，而无需等待调用的异步方法完成。 调用方必须独立于异步方法生成的任何值或异常。
               返回 void 的异步方法的调用方无法捕获从该方法引发的异常，且此类未经处理的异常可能会导致应用程序故障。 如果返回 Task 或 Task<TResult> 的方法引发异常，则该异常存储在返回的任务中。 等待任务时，将重新引发异常。 因此，请确保可以产生异常的任何异步方法都具有返回类型 Task 或 Task<TResult>，并确保会等待对方法的调用。
     * 
     * Task：不包含 return 语句的异步方法或包含不返回操作数的 return 语句的异步方法通常具有返回类型 Task。 如果此类方法同步运行，它们将返回 void。 如果在异步方法中使用Task 返回类型，调用方法可以使用 await 运算符暂停调用方的完成，直至被调用的异步方法结束。
     * 
     * Task<TResult> ：返回类型用于某种异步方法，此异步方法包含 return (C#) 语句，其中操作数是 TResult。
     * FromResult 异步方法是返回字符串的操作的占位符。
     **/
    class Program
    {

        static TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
        private static Random rnd;
        static async Task Main(string[] args)
        {
            tcs = new TaskCompletionSource<bool>(); 
            var secondHandlerFinished = tcs.Task;

            var button = new NaiveButton();
            button.Clicked += Button_Clicked_1;
            button.Clicked += Button_Clicked_2_Async;
            button.Clicked += Button_Clicked_3;

            Console.WriteLine("About to click a button...");
            button.Click();
            Console.WriteLine("Button's Click method returned.");

            await secondHandlerFinished;
            var today = await Task.FromResult(DateTime.Now.DayOfWeek.ToString());
            //await SumPageSizesAsync();

            //Console.WriteLine(msg);
        }
        private static async IAsyncEnumerable<string> ReadWordsFromStream()
        {
            string data =
            @"This is a line of text.
      Here is the second line of text.
      And there is one more for good measure.
      Wait, that was the penultimate line.";

            using var readStream = new StringReader(data);

            string line = await readStream.ReadLineAsync();
            while (line != null)
            {
                var words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    yield return word;
                }
                line = await readStream.ReadLineAsync();
            }
        }
        private static async ValueTask<int> GetDiceRoll()
        {
            Console.WriteLine("...Shaking the dice...");
            int roll1 = await Roll();
            int roll2 = await Roll();
            return roll1 + roll2;
        }

        private static async ValueTask<int> Roll()
        {
            if (rnd == null)
                rnd = new Random();

            await Task.Delay(500);
            int diceRoll = rnd.Next(1, 7);
            return diceRoll;
        }

        private async static Task SumPageSizesAsync()
        {
            // string msg = "";
            // List<string> urlList = SetUpURLList();

            // var total = 0;
            // foreach (var url in urlList)
            // {
            //     byte[] urlContents = await GetURLContentsAsync(url);

            //     DisplayResults(url, urlContents);
            //     total += urlContents.Length;
            // }

            //msg += $"\r\n\r\nTotal bytes returned:  {total}\r\n";
            // Console.WriteLine(msg);

            IEnumerable<Task<int>> downloadTasksQuery =from url in SetUpURLList() select ProcessURLAsync(url);
            Task<int>[] downloadTasks = downloadTasksQuery.ToArray();
            Console.WriteLine("do something。。。");
            Console.WriteLine($"You rolled {GetDiceRoll().Result}");
            IAsyncEnumerable<string> list = ReadWordsFromStream();
            await foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            int[] lengths = await Task.WhenAll(downloadTasks);
            int total = lengths.Sum();
            Console.WriteLine($"\r\n\r\nTotal bytes returned:  {total}\r\n");
        }

        private  async static Task SumPageSizes2Async()
        {
            // Make a list of web addresses.
            List<string> urlList = SetUpURLList();

            // Declare an HttpClient object and increase the buffer size. The
            // default buffer size is 65,536.
            HttpClient client = new HttpClient() { MaxResponseContentBufferSize = 1000000 };

            // Create a query.
            IEnumerable<Task<int>> downloadTasksQuery =
                from url in urlList select ProcessURLAsync(url, client);

            // Use ToArray to execute the query and start the download tasks.
            Task<int>[] downloadTasks = downloadTasksQuery.ToArray();

            // You can do other work here before awaiting.

            // Await the completion of all the running tasks.
            int[] lengths = await Task.WhenAll(downloadTasks);

            //// The previous line is equivalent to the following two statements.
            //Task<int[]> whenAllTask = Task.WhenAll(downloadTasks);
            //int[] lengths = await whenAllTask;

            int total = lengths.Sum();

            //var total = 0;
            //foreach (var url in urlList)
            //{
            //    // GetByteArrayAsync returns a Task<T>. At completion, the task
            //    // produces a byte array.
            //    byte[] urlContent = await client.GetByteArrayAsync(url);

            //    // The previous line abbreviates the following two assignment
            //    // statements.
            //    Task<byte[]> getContentTask = client.GetByteArrayAsync(url);
            //    byte[] urlContent = await getContentTask;

            //    DisplayResults(url, urlContent);

            //    // Update the total.
            //    total += urlContent.Length;
            //}

            // Display the total count for all of the web addresses.
            Console.WriteLine($"\r\n\r\nTotal bytes returned:  {total}\r\n");
                
        }

        private static  List<string> SetUpURLList()
        {
            var urls = new List<string>
            {
                "https://msdn.microsoft.com/library/windows/apps/br211380.aspx",
                "https://msdn.microsoft.com",
                "https://msdn.microsoft.com/library/hh290136.aspx",
                "https://msdn.microsoft.com/library/ee256749.aspx",
                "https://msdn.microsoft.com/library/hh290138.aspx",
                "https://msdn.microsoft.com/library/hh290140.aspx",
                "https://msdn.microsoft.com/library/dd470362.aspx",
                "https://msdn.microsoft.com/library/aa578028.aspx",
                "https://msdn.microsoft.com/library/ms404677.aspx",
                "https://msdn.microsoft.com/library/ff730837.aspx"
            };
            return urls;
        }

        private async static Task<byte[]> GetURLContentsAsync(string url)
        {
            var content = new MemoryStream();

            var webReq = WebRequest.Create(url) as HttpWebRequest;
            //这里将GetResponse()改成异步ßß
            //await 运算符将当前方法 GetURLContents 的执行挂起，直到完成等待的任务为止。(添加了 await 运算符，所以会发生编译器错误。 该运算符仅可在使用 async 修饰符标记的方法中使用。)
            //同时，控制权返回给当前方法的调用方。 
            //在此示例中，当前方法是 GetURLContents，调用方是 SumPageSizes。 任务完成时，承诺的 WebResponse 对象作为等待的任务的值生成，并分配给变量 response。
            using (WebResponse response = await webReq.GetResponseAsync())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    //responseStream.CopyTo(content);
                    //CopyTo 或 CopyToAsync 方法复制字节到其参数 content，并且不返回有意义的值。 在同步版本中，对 CopyTo 的调用是不返回值的简单语句。 异步版本 CopyToAsync 返回 Task。 任务函数类似“Task(void)”，并让该方法能够等待。 应用 Await 或 await 到对 CopyToAsync 的调用，如下列代码所示。
                    await responseStream.CopyToAsync(content);
                }
            }

            return content.ToArray();
        }

        private static  void DisplayResults(string url, byte[] content)
        {
            string msg2 = "";
            var bytes = content.Length;
            var displayURL = url.Replace("https://", "");
            msg2 += $"\n{displayURL,-58} {bytes,8}";
            Console.WriteLine(msg2);
        }

        private static async Task<int> ProcessURLAsync(string url)
        {
            var byteArray = await GetURLContentsAsync(url);
            DisplayResults(url, byteArray);
            return byteArray.Length;
        }

        private static async Task<int> ProcessURLAsync(string url, HttpClient client)
        {
            var byteArray = await client.GetByteArrayAsync(url);
            DisplayResults(url, byteArray);
            return byteArray.Length;
        }
        private async Task CreateMultipleTasksAsync()
        {
            HttpClient client =new HttpClient() { MaxResponseContentBufferSize = 1000000 };
            
            //并发访问三个网站
            Task<int> download1 =
                ProcessURLAsync("https://msdn.microsoft.com", client);
            Task<int> download2 =
                ProcessURLAsync("https://msdn.microsoft.com/library/hh156528(VS.110).aspx", client);
            Task<int> download3 =
                ProcessURLAsync("https://msdn.microsoft.com/library/67w7t67f.aspx", client);

            // Await each task.  
            int length1 = await download1;
            int length2 = await download2;
            int length3 = await download3;

            int total = length1 + length2 + length3;
            Console.WriteLine($"\r\n\r\nTotal bytes returned:  {total}\r\n");
        }

        private static void Button_Clicked_1(object sender, EventArgs e)
        {
            Console.WriteLine("   Handler 1 is starting...");
            Task.Delay(100).Wait();
            Console.WriteLine("   Handler 1 is done.");
        }

        private static async void Button_Clicked_2_Async(object sender, EventArgs e)
        {
            Console.WriteLine("   Handler 2 is starting...");
            Task.Delay(100).Wait();
            Console.WriteLine("   Handler 2 is about to go async...");
            //这里等待时，事件3已经触发
            await Task.Delay(500);
            Console.WriteLine("   Handler 2 is done.");
            //告诉主线程事件已经完成
            tcs.SetResult(true);
        }

        private static void Button_Clicked_3(object sender, EventArgs e)
        {
            Console.WriteLine("   Handler 3 is starting...");
            Task.Delay(100).Wait();
            Console.WriteLine("   Handler 3 is done.");
        }
    }


    public class NaiveButton
    {
        public event EventHandler Clicked;

        public void Click()
        {
            Console.WriteLine("Somebody has clicked a button. Let's raise the event...");
            Clicked?.Invoke(this, EventArgs.Empty);
            Console.WriteLine("All listeners are notified.");
        }
    }
}
