using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Linq
{
    public sealed class Set
    {
        public static void Distinct()
        {
            string[] planets = { "Mercury", "Venus", "Venus", "Earth", "Mars", "Earth" };

            IEnumerable<string> query = from planet in planets.Distinct()//删除集合中的重复值。
                                        select planet;

            foreach (var str in query)
            {
                Console.WriteLine(str);
            }
        }
        public static void Except()
        {
            string[] planets1 = { "Mercury", "Venus", "Earth", "Jupiter" };
            string[] planets2 = { "Mercury", "Earth", "Mars", "Jupiter" };

            IEnumerable<string> query = from planet in planets1.Except(planets2)//返回的序列只包含位于第一个输入序列但不位于第二个输入序列的元素。
                                        select planet;

            foreach (var str in query)
            {
                Console.WriteLine(str);
            }
        }

        public static void Intersect()
        {
            string[] planets1 = { "Mercury", "Venus", "Earth", "Jupiter" };
            string[] planets2 = { "Mercury", "Earth", "Mars", "Jupiter" };

            IEnumerable<string> query = from planet in planets1.Intersect(planets2)//返回的序列包含两个输入序列共有的元素。
                                        select planet;

            foreach (var str in query)
            {
                Console.WriteLine(str);
            }
        }

        public static void Union()
        {
            string[] planets1 = { "Mercury", "Venus", "Earth", "Jupiter" };
            string[] planets2 = { "Mercury", "Earth", "Mars", "Jupiter" };

            IEnumerable<string> query = from planet in planets1.Union(planets2)//返回的序列包含两个输入序列的唯一元素。
                                        select planet;

            foreach (var str in query)
            {
                Console.WriteLine(str);
            }
        }
    }
}
