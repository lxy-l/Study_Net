namespace ConsoleApp_Delegate
{
    /// <summary>
    /// 做某事委托（无参无返回值）
    /// </summary>
    public delegate void DoSomething();
    /// <summary>
    /// 做某事委托（无参有返回值）
    /// </summary>
    public delegate int DoSomethingReturn();
    /// <summary>
    /// 做更多（有参无返回）
    /// </summary>
    /// <param name="age"></param>
    /// <param name="name"></param>
    public delegate void DoMore(int age, string name);
    /// <summary>
    /// 做更多（有参有返回）
    /// </summary>
    /// <param name="age"></param>
    /// <param name="name"></param>
    public delegate int DoMoreReturn(int age, string name);
    /// <summary>
    /// 委托方法类
    /// </summary>
    public sealed class CommonDelegate
    {
        public static void DoSomethingMethod()
        {
            Console.WriteLine($"Sub-Start【ThreadId={Thread.CurrentThread.ManagedThreadId}】：{DateTime.Now}");
            Thread.Sleep(3000);
            Console.WriteLine($"Sub-End【ThreadId={Thread.CurrentThread.ManagedThreadId}】：{DateTime.Now}");
        }

        public static int DoSomethingReturnMethod()
        {
            Console.WriteLine("Sub-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
            Thread.Sleep(3000);
            Console.WriteLine("Sub-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
            return 99;
        }

        public static void DoMoreMethod(int age, string name)
        {
            Console.WriteLine("Sub-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
            Console.WriteLine("age={0},name={1}", age, name);
            Thread.Sleep(3000);
            Console.WriteLine("Sub-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
        }

        public static int DoMoreReturnMethod(int age, string name)
        {
            Console.WriteLine("Sub-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
            Console.WriteLine("age={0},name={1}", age, name);
            Thread.Sleep(3000);
            Console.WriteLine("Sub-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
            return 99;
        }
    }
}
