using System;

namespace ConsoleApp1
{
    /// <summary>
    /// C#9.0与.net5.0
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            DateTime now = DateTime.Now;
            DateTime time = new(2020,12,12);

            #region record与class的区别

            Cat pet1 = new Cat { Nick = "A", Name = "A", Age = 11,Now= now };
            Cat pet2 = new Cat { Nick = "A", Name = "A", Age = 11,Now= now };

            CatClass pet3 = new CatClass { Nick = "A", Name = "A", Age = 11 };
            CatClass pet4 = new CatClass { Nick = "A", Name = "A", Age = 11 };

            Console.WriteLine("record是否是同一只？{0}",pet1==pet2);
            Console.WriteLine("class:是否是同一只？{0}", pet3==pet4);
            /**
             record 类型让你省去了重写相等比较（重写 Equals、GetHashCode 等方法或重载运算符）的逻辑。
            实际上，代码在编译后 record 类型也是一个类，但自动实现了成员相等比较的逻辑。以前你要手动去折腾的事现在全交给编译器去干。
             */
            #endregion

            #region 模式匹配switch

            switch (pet1)
            {
                case { Age:>10 }:
                    Console.WriteLine("老了");
                    break;
                case { Age:<6}:
                    Console.WriteLine("小了");
                    break;
                default:
                    Console.WriteLine("SB");
                    break;
            }

            pet1.Age = 7;
            string message = pet1 switch
            {
                { Age: > 10 } => "老了",
                { Age: < 6 } => "小了",
                _ => "未知"
            };
            Console.WriteLine(message);


            if (pet1 is { Age :<6})
            {
                Console.WriteLine("小了");
            }
            else
            {
                Console.WriteLine("SB");
            }

            //仅用于常量的匹配比较
            //if (pet1 is{ Now:>DateTime.Now })
            //{

            //}

            object n = 12345678L;
            if (n is long x)
            {
                //自动转换，不用强转
                Console.WriteLine(x);
            }

            #endregion

            #region init

            Dog dog1 = new Dog { Age=10,Name="HH",No=12};

            //dog1.Age = 10;

            #endregion

        }
    }
}
