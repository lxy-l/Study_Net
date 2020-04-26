using System;
using System.Threading.Tasks;
using System.Windows;

// Add a using directive and a reference for System.Net.Http;
using System.Net.Http;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace AsyncFirstExample
{
    public partial class MainWindow : Window
    {
        CancellationTokenSource cts;
        private Task pendingWork = null;
        private char group = (char)('A' - 1);
        private List<string> SetUpURLList()
        {
            List<string> urls = new List<string>
            {
                "https://msdn.microsoft.com",
                "https://msdn.microsoft.com/library/hh290138.aspx",
                "https://msdn.microsoft.com/library/hh290140.aspx",
                "https://msdn.microsoft.com/library/dd470362.aspx",
                "https://msdn.microsoft.com/library/aa578028.aspx",
                "https://msdn.microsoft.com/library/ms404677.aspx",
                "https://msdn.microsoft.com/library/ff730837.aspx"
            };
            return urls;
        }
        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            startButton.IsEnabled = false;
            group = (char)(group + 1);
            resultsTextBox.Text += $"\r\n\r\n#Starting group {group}.";

            if (cts != null)
            {
                cts.Cancel();
            }
            CancellationTokenSource newCTS = new CancellationTokenSource();
            cts = newCTS;//将 cts 设置为表示当前进程的不同值。
            //resultsTextBox.Clear();
            try
            {

                //在一段时间后取消
                //cts.CancelAfter(2500);
                char finishedGroup = await AccessTheWebAsync(group);
                resultsTextBox.Text += $"\r\n\r\n#Group {finishedGroup} is complete.\r\n";
            }
            catch (OperationCanceledException)
            {
                resultsTextBox.Text += "\r\nDownload canceled.\r\n";
            }
            catch (Exception)
            {
                resultsTextBox.Text += "\r\nDownload failed.\r\n";
            }
            finally
            {
                startButton.IsEnabled = true;
            }
            if (cts == newCTS)
                cts = null;
        }
        async Task<int> ProcessURLAsync(string url, HttpClient client, CancellationToken ct)
        {
            HttpResponseMessage response = await client.GetAsync(url, ct);
            byte[] urlContents = await response.Content.ReadAsByteArrayAsync();
            return urlContents.Length;//下载完成返回大小
        }
        async Task AccessTheWebAsync(CancellationToken ct)
        {
            HttpClient client = new HttpClient();

            IEnumerable<Task<int>> downloadTasksQuery =from url in SetUpURLList() select ProcessURLAsync(url, client, ct);

            List<Task<int>> downloadTasks = downloadTasksQuery.ToList();//所有下载任务开始

            while (downloadTasks.Count > 0)//下载数>0进行下载
            {
                Task<int> firstFinishedTask = await Task.WhenAny(downloadTasks);//执行若干个任务，只需要对其中任意一个的完成进行响应。这主要用于：对一个操作进行多种独立的尝试，只要一个尝试完成，任务就算完成。例如，同时向多个 Web 服务询问股票价格，但是只关心第一个响应的。

                downloadTasks.Remove(firstFinishedTask);//下载完成移除任务

                int length = await firstFinishedTask;
                resultsTextBox.Text += $"\r\nLength of the download:  {length}";
            }
        }
        private async Task<char> AccessTheWebAsync(char grp)
        {
            HttpClient client = new HttpClient();
            List<string> urlList = SetUpURLList();
            Task<byte[]>[] getContentTasks = urlList.Select(url => client.GetByteArrayAsync(url)).ToArray();
            pendingWork = FinishOneGroupAsync(urlList, getContentTasks, grp);
            resultsTextBox.Text += $"\r\n#Task assigned for group {grp}. Download tasks are active.\r\n";
            await pendingWork;
            return grp;
        }
        private async Task FinishOneGroupAsync(List<string> urls, Task<byte[]>[] contentTasks, char grp)
        {
            if (pendingWork != null) await pendingWork;
            int total = 0;
            for (int i = 0; i < contentTasks.Length; i++)
            {
                byte[] content = await contentTasks[i];
                DisplayResults(urls[i], content, i, grp);
                total += content.Length;
            }
            resultsTextBox.Text +=
                $"\r\n\r\nTOTAL bytes returned:  {total}\r\n";
        }

        private void DisplayResults(string v, byte[] content, int i, char grp)
        {
            resultsTextBox.Text += $"\r\n{grp}-{i}:{v}\t=>大小：{content.Length}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cts!=null)
            {
                cts.Cancel();
            }
        }
    }
}