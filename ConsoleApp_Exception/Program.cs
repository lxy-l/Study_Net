using System;

namespace ConsoleApp_Exception
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string s = null;
                Console.WriteLine($"[{DateTime.Now,-20:d}] Hour [{DateTime.Now,-10:HH}] [{1063.342,15:N2}] feet");
                Console.WriteLine(s.Length);

            }
            catch (Exception e) when (LogException(e))
            {
                Console.WriteLine("Exception"+e.Message);
            }
            Console.WriteLine("Exception must have been handled");
        }
        private static bool LogException(Exception e)
        {
            //异常处理
            Console.WriteLine($"\tIn the log routine. Caught {e.GetType()}");
            Console.WriteLine($"\tMessage: {e.Message}");
            return true;//如果True进入catch内执行代码否则跳出
        }
    }
}
