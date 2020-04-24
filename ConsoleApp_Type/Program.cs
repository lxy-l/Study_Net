using System;


namespace ConsoleApp_Type
{
    class Program
    {
        /*
         *  
          
        1.Equals()
            1.==在比对值类型时，如果二者值相等，则返回true，否则返回false。
            2.==在比对引用类型时，如果二者引用的是同一个对象，则返回true，否则返回false。
            3.string类型的Equals()方法，在不重写的情况下，与==没区别。
            4.微软重写了string的Equals()方法，使得这个方法比对的是string

          2.结构
            结构可带有方法、字段、索引、属性、运算符方法和事件。
            结构可定义构造函数，但不能定义析构函数。但是，您不能为结构定义无参构造函数。无参构造函数(默认)是自动定义的，且不能被改变。
            与类不同，结构不能继承其他的结构或类。
            结构不能作为其他结构或类的基础结构。
            结构可实现一个或多个接口。
            结构成员不能指定为 abstract、virtual 或 protected。
            当您使用 New 操作符创建一个结构对象时，会调用适当的构造函数来创建结构。与类不同，结构可以不使用 New 操作符即可被实例化。
            如果不使用 New 操作符，只有在所有的字段都被初始化之后，字段才被赋值，对象才被使用。
            结构是值类型，类是引用类型：类的对象是存储在堆空间中，结构存储在栈中。堆空间大，但访问速度较慢，栈空间小，访问速度相对更快
         * 
         */
        static void Main(string[] args)
        {
            #region String
            string str1 = "1";
            string str2 = "1";
            Console.WriteLine($"str1 equals str2:{str1.Equals(str2)};\nstr1 == str2 :{str1 == str2}");
            string str3 = str1;
            Console.WriteLine($"str1 equals str3:{str1.Equals(str3)};\nstr1 == str3 :{str1 == str3};\nstr2 equals str3:{str2.Equals(str3)}\nstr2 == str3 :{str2 == str3}");
            string[] words = new string[]
            {
                            // index from start    index from end
                "The",      // 0                   ^9
                "quick",    // 1                   ^8
                "brown",    // 2                   ^7
                "fox",      // 3                   ^6
                "jumped",   // 4                   ^5
                "over",     // 5                   ^4
                "the",      // 6                   ^3
                "lazy",     // 7                   ^2
                "dog"       // 8                   ^1
            };              // 9 (or words.Length) ^0
            Console.WriteLine($"The last word is {words[^1]}");
            string[] quickBrownFox = words[1..4];
            foreach (var word in quickBrownFox)
                Console.Write($"< {word} >");
            Console.WriteLine("\n\n");
            #endregion

            #region Int
            int num = 1;
            int Maxnum = int.MaxValue;
            int Minnum = int.MinValue;
            int.Parse("1");
            int.TryParse("2", out num);
            Console.WriteLine(num);
            Console.WriteLine($"MaxNum:{Maxnum};Minnum:{Minnum}");
            Console.WriteLine("\n\n");
            #endregion

            #region DateTime
            DateTime nowtime = DateTime.Now;
            DateTime utctime = DateTime.UtcNow;

            //DateTime localTime=DateTime.Now

            bool eq = DateTime.Equals(nowtime, utctime);

            Console.WriteLine($"NowDateTime:{nowtime}\nUtcDateTime:{utctime};\nequals:{eq}");
            Console.WriteLine(Daytime.GetTime().ToLocalTime());
            Console.WriteLine("\n\n");
            #endregion

            #region Enum
            Console.WriteLine((int)QuestionType.YesNo);
            Console.WriteLine((QuestionType)Enum.Parse(typeof(QuestionType), "1"));
            #endregion


            float num1 = float.MaxValue;
            decimal num2 = decimal.MaxValue;
            Console.WriteLine(num1);
            Console.WriteLine(num2);


            Values values = new Values("sdf","23er");
            //values.FirstName = "Roy"; values.LastName = "Stewart";
            Console.WriteLine(values.FirstName);
            Console.WriteLine(values.ToString());



        }

        public enum QuestionType
        {
            YesNo =1,
            Number,
            Text
        }

        public class TollCalculator
        {
            public decimal CalculateToll(object vehicle) =>
                vehicle switch
                {
                    { } => throw new ArgumentException(message: "Not a known vehicle type", paramName: nameof(vehicle)),
                    null => throw new ArgumentNullException(nameof(vehicle))
                };
        }

        public struct Values
        {
            public Values(string fname,string lname)
            {
                FirstName = fname;
                LastName = lname;
            }

            public string FirstName;
            public string LastName;
            public override string ToString()
            {
                return FirstName + ":"+LastName;
            }
        }
    }
}
