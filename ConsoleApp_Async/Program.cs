using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp_Async
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Coffee cup = PourCoffee();
            Console.WriteLine("coffee is ready");
            var eggsTask = FryEggsAsync(2);
            var baconTask = FryBaconAsync(3);
            var toastTask = MakeToastWithButterAndJamAsync(2);

            var allTasks = new List<Task> { eggsTask, baconTask, toastTask };
            while (allTasks.Any())
            {
                Task finished = await Task.WhenAny(allTasks);
                if (finished == eggsTask)
                {
                    Console.WriteLine("eggs are ready");
                }
                else if (finished == baconTask)
                {
                    Console.WriteLine("bacon is ready");
                }
                else if (finished == toastTask)
                {
                    Console.WriteLine("toast is ready");
                }
                allTasks.Remove(finished);
            }
            Juice oj = PourOJ();
            Console.WriteLine("oj is ready");
            Console.WriteLine("Breakfast is ready!");

            async Task<Toast> MakeToastWithButterAndJamAsync(int number)
            {
                var toast = await ToastBreadAsync(number);
                ApplyButter(toast);
                ApplyJam(toast);
                return toast;
            }
        }

        private static void ApplyJam(object toast)
        {
            Console.WriteLine("加果酱");
            Task.Delay(1000);
        }

        private static void ApplyButter(object toast)
        {
            Console.WriteLine("加黄油");
            Task.Delay(1000);
        }

        private static Task<Toast> ToastBreadAsync(int number)
        {
            Console.WriteLine($"开始烤{number}面包");
            Task.Delay(15000);
            return Task.FromResult(new Toast());
        }

        private static Juice PourOJ()
        {
            Console.WriteLine("开始倒一杯橙汁");
            Task.Delay(20000);
            return new Juice();
        }

        private static Task FryBaconAsync(int v)
        {
            Console.WriteLine($"开始煎两{v}培根");
            Task.Delay(8000);
            return Task.CompletedTask;
        }

        private static Task FryEggsAsync(int v)
        {
            Console.WriteLine($"开始煎两{v}鸡蛋");
            Task.Delay(5000);
            return Task.CompletedTask;
        }

        private static Coffee PourCoffee()
        {
            
            Console.WriteLine("开始倒一杯咖啡");
            Task.Delay(8000);
            return new Coffee();
        }
    }
}
