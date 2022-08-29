using System.Threading;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp_IEnumerable
{
    class Program
    {
        static IEnumerable<string> Suits()
        {
            yield return "梅花";
            yield return "方块";
            yield return "红桃";
            yield return "黑桃";
        }

        static IEnumerable<string> Ranks()
        {
            yield return "2";
            yield return "3";
            yield return "4";
            yield return "5";
            yield return "6";
            yield return "7";
            yield return "8";
            yield return "9";
            yield return "10";
            yield return "J";
            yield return "Q";
            yield return "K";
            yield return "A";
        }

        static int[,] array2Da = new int[3, 2] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
        static async Task Main(string[] args)
        {
            //var startingDeck = (from s in Suits()/*.LogQuery("Suit Generation")*/
            //                    from r in Ranks()/*.LogQuery("Value Generation")*/
            //                    select new { Suit = s, Rank = r })
            //  //.LogQuery("Starting Deck")
            //  .ToArray();

            //foreach (var c in startingDeck)
            //{
            //    Console.WriteLine(c);
            //}

            //Console.WriteLine("开始洗牌：");

            //var times = 0;
            //var shuffle = startingDeck;

            //do
            //{
            //    /*
            //    shuffle = shuffle.Take(26)
            //        .LogQuery("Top Half")
            //        .InterleaveSequenceWith(shuffle.Skip(26).LogQuery("Bottom Half"))
            //        .LogQuery("Shuffle")
            //        .ToArray();
            //    */

            //    shuffle = shuffle.Skip(26)
            //        //.LogQuery("Bottom Half")
            //        .InterleaveSequenceWith(shuffle.Take(26)/*.LogQuery("Top Half")*/)
            //        //.LogQuery("Shuffle")
            //        .ToArray();

            //    foreach (var c in shuffle)
            //    {
            //        Console.WriteLine(c);
            //    }

            //    times++;
            //    Console.WriteLine(times);
            //} while (!startingDeck.SequenceEquals(shuffle));

            //Console.WriteLine(times);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            // await foreach (var number in GenerateSequence())
            // {
            //     Console.WriteLine(number);
            // }
            // stopwatch.Stop();
            // System.Console.WriteLine(stopwatch.ElapsedMilliseconds);
            // stopwatch.Restart();
            foreach (var item in GenerateSequence2())
            {
                Console.WriteLine(item);
            }
            stopwatch.Stop();
            System.Console.WriteLine(stopwatch.ElapsedMilliseconds);
            // int num = Find(array2Da, x =>
            //  {
            //      return x > 3 && x < 5;
            //  });
            // Console.WriteLine(num);
            // num = 666;

            // ref var item = ref Find(array2Da, val => val == 6);
            // Console.WriteLine(item);
            // item = 24;
        }

        public static async IAsyncEnumerable<int> GenerateSequence()
        {
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(100);
                yield return i;
            }
        }

        public static IEnumerable<int> GenerateSequence2()
        {
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(100);
                yield return i;
            }
        }


        public static ref int Find(int[,] matrix, Func<int, bool> predicate)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (predicate(matrix[i, j]))
                        return ref matrix[i, j];
            throw new InvalidOperationException("Not found");
        }

        public static async Task<string> MakeRequestAndLogFailures()
        {
            await logMethodEntrance();
            var client = new System.Net.Http.HttpClient();
            var streamTask = client.GetStringAsync("https://localHost:10000");
            try
            {
                var responseText = await streamTask;
                return responseText;
            }
            catch (HttpRequestException e) when (e.Message.Contains("301"))
            {
                await logError("Recovered from redirect", e);
                return "Site Moved";
            }
            finally
            {
                await logMethodExit();
                client.Dispose();
            }
        }

        private static Task logMethodExit()
        {
            throw new NotImplementedException();
        }

        private static Task logError(string v, HttpRequestException e)
        {
            throw new NotImplementedException();
        }

        private static Task logMethodEntrance()
        {
            Console.WriteLine("OK");
            return Task.CompletedTask;
        }
    }
}
