using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_AsyncFile
{
    class Program
    {
        static async Task Main(string[] args)
        {
             await ProcessWriteAsync();
        }
        public static async Task ProcessWriteAsync()
        {
            string filePath = @"temp2.txt";
            string text = "Hello World\r\n";

            await WriteTextAsync(filePath, text);
            Console.WriteLine("写入完成");
            await ProcessReadAsync();
            Console.WriteLine("读取完成");
            await ProcessWriteMultAsync();
            Console.WriteLine("多文件写入完成");
        }

        private static  async Task WriteTextAsync(string filePath, string text)
        {
            byte[] encodedText = Encoding.Unicode.GetBytes(text);//编码

            using (FileStream sourceStream = new FileStream(filePath,
                FileMode.Append, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);//等待写入完成
            };//写入
        }
        public static  async Task ProcessReadAsync()
        {
            string filePath = @"temp2.txt";

            if (File.Exists(filePath) == false)
            {
                Console.WriteLine("file not found: " + filePath);
            }
            else
            {
                try
                {
                    string text = await ReadTextAsync(filePath);
                    Console.WriteLine(text);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private static  async Task<string> ReadTextAsync(string filePath)
        {
            using (FileStream sourceStream = new FileStream(filePath,
                FileMode.Open, FileAccess.Read, FileShare.Read,
                bufferSize: 4096, useAsync: true))
            {
                StringBuilder sb = new StringBuilder();

                byte[] buffer = new byte[0x1000];
                int numRead;
                while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string text = Encoding.Unicode.GetString(buffer, 0, numRead);
                    sb.Append(text);
                }

                return sb.ToString();
            }
        }
        public static  async Task ProcessWriteMultAsync()
        {
            string folder = @"tempfolder\";
            List<Task> tasks = new List<Task>();
            List<FileStream> sourceStreams = new List<FileStream>();

            try
            {
                for (int index = 1; index <= 10; index++)
                {
                    string text = "In file " + index.ToString() + "\r\n";
                    string fileName = "thefile" + index.ToString("00") + ".txt";
                    string filePath = folder + fileName;
                    byte[] encodedText = Encoding.Unicode.GetBytes(text);//编码

                    FileStream sourceStream = new FileStream(filePath,
                        FileMode.Append, FileAccess.Write, FileShare.None,
                        bufferSize: 4096, useAsync: true);//生成文件流

                    Task theTask = sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
                    sourceStreams.Add(sourceStream);//添加每个文件文件流
                    tasks.Add(theTask);//添加任务
                }

                await Task.WhenAll(tasks);//等待所有任务完成
            }

            finally
            {
                foreach (FileStream sourceStream in sourceStreams)
                {
                    sourceStream.Close();
                }
            }
        }

    }
}
