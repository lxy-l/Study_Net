using System;

namespace ConsoleApp_Attribute
{
    [Author("NB", version = 2.0)]
    class Program
    {
        private static void PrintAuthorInfo(Type t)
        {
            Console.WriteLine("Author information for {0}", t);
            Attribute[] attrs = Attribute.GetCustomAttributes(t);  // 反射
            foreach (Attribute attr in attrs)
            {
                if (attr is Author)
                {
                    Author a = (Author)attr;
                    Console.WriteLine("   {0}, version {1:f}", a.GetName(), a.version);
                }
            }
        }
        static void Main(string[] args)
        {
            PrintAuthorInfo(typeof(Program));
        }
    }
}
