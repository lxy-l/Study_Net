using System;

namespace ConsoleApp_Type
{
    class Program
    {
        /*
         * 1.==在比对值类型时，如果二者值相等，则返回true，否则返回false。

            2.==在比对引用类型时，如果二者引用的是同一个对象，则返回true，否则返回false。

            3.string类型的Equals()方法，在不重写的情况下，与==没区别。

            4.微软重写了string的Equals()方法，使得这个方法比对的是string
         * 
         */
        static void Main(string[] args)
        {
            string str1 = "1";
            string str2 = "1";
            Console.WriteLine($"str1 equals str2:{str1.Equals(str2)};\nstr1 == str2 :{str1==str2}");
            string str3 = str1;
            Console.WriteLine($"str1 equals str3:{str1.Equals(str3)};\nstr1 == str3 :{str1 == str3};\nstr2 equals str3:{str2.Equals(str3)}\nstr2 == str3 :{str2 == str3}");

            DateTime nowtime = DateTime.Now;
            DateTime utctime = DateTime.UtcNow;

            //DateTime localTime=DateTime.Now

            bool eq = DateTime.Equals(nowtime, utctime);

            Console.WriteLine($"NowDateTime:{nowtime}\nUtcDateTime:{utctime};\nequals:{eq}");
            Console.WriteLine(Daytime.GetTime().ToLocalTime());
        }
    }
}
