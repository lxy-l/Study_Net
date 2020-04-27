using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Linq
{
    class Market
    {
        public string Name { get; set; }
        public string[] Items { get; set; }

        public static void ExampleAll()
        {
            List<Market> markets = new List<Market>
            {
                new Market { Name = "Emily's", Items = new string[] { "kiwi", "cheery", "banana" } },
                new Market { Name = "Kim's", Items = new string[] { "melon", "mango", "olive" } },
                new Market { Name = "Adam's", Items = new string[] { "kiwi", "apple", "orange" } },
            };

            // Determine which market have all fruit names length equal to 5
            IEnumerable<string> names = from market in markets
                                        where market.Items.All(item => item.Length == 5)
                                        select market.Name;

            foreach (string name in names)
            {
                Console.WriteLine($"{name} market");
            }

            // This code produces the following output:
            //
            // Kim's market
        }

        public static void ExampleAny()
        {
            List<Market> markets = new List<Market>
            {
                new Market { Name = "Emily's", Items = new string[] { "kiwi", "cheery", "banana" } },
                new Market { Name = "Kim's", Items = new string[] { "melon", "mango", "olive" } },
                new Market { Name = "Adam's", Items = new string[] { "kiwi", "apple", "orange" } },
            };

            // Determine which market have any fruit names start with 'o'
            IEnumerable<string> names = from market in markets
                                        where market.Items.Any(item => item.StartsWith("o"))
                                        select market.Name;

            foreach (string name in names)
            {
                Console.WriteLine($"{name} market");
            }
        }

        public static void ExampleContains()
        {
            List<Market> markets = new List<Market>
            {
                new Market { Name = "Emily's", Items = new string[] { "kiwi", "cheery", "banana" } },
                new Market { Name = "Kim's", Items = new string[] { "melon", "mango", "olive" } },
                new Market { Name = "Adam's", Items = new string[] { "kiwi", "apple", "orange" } },
            };

            // Determine which market contains fruit names equal 'kiwi'
            IEnumerable<string> names = from market in markets
                                        where market.Items.Contains("kiwi")
                                        select market.Name;

            foreach (string name in names)
            {
                Console.WriteLine($"{name} market");
            }

        }
    }
}