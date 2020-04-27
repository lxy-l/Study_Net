using System;
using System.Reflection;

namespace ConsoleApp_Reflection
{
    /*
     反射提供描述程序集、模块和类型的对象（Type 类型）。 
        可以使用反射动态地创建类型的实例，将类型绑定到现有对象，或从现有对象中获取类型，然后调用其方法或访问器字段和属性。 
        如果代码中使用了特性，可以利用反射来访问它们。
     反射在以下情况下很有用：
                需要访问程序元数据中的特性时。 有关详细信息，请参阅检索存储在特性中的信息。
                检查和实例化程序集中的类型。
                在运行时构建新类型。 使用 System.Reflection.Emit 中的类。
                执行后期绑定，访问在运行时创建的类型上的方法。 请参阅主题 “动态加载和使用类型”。
     */
    class Program
    {
        static void Main(string[] args)
        {
            // Using GetType to obtain type information:
            int i = 42;
            Type type = i.GetType();
            Console.WriteLine(type);
            // Using Reflection to get information of an Assembly:
            Assembly info = typeof(int).Assembly;
            Console.WriteLine(info);
        }
    }
}
