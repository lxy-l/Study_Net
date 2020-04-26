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
     **/
    class Program
    {
        static async Task Main(string[] args)
        {
            await SumPageSizesAsync();
            //Console.WriteLine(msg);
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

            var webReq = (HttpWebRequest)WebRequest.Create(url);
            //这里将GetResponse()改成异步
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
    }
}
