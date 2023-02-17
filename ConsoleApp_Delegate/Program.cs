using ConsoleApp_Delegate;

TakesAwhileDel dl = TakesAwhile;
dl.BeginInvoke(1, 6000, AsyncCallbackImpl, dl);
Thread.Sleep(1000);

Console.WriteLine("Main-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
DoSomething doSomething = CommonDelegate.DoSomethingMethod;
doSomething.BeginInvoke(null, null);
Console.WriteLine("Main-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
Console.ReadLine();


void AsyncCallbackImpl(IAsyncResult ar)
{
    TakesAwhileDel dl = ar.AsyncState as TakesAwhileDel;
    string re = dl.EndInvoke(ar);
    Console.WriteLine("结果{0}", re);
    //TakesAwhileDel d2 = TakesAwhile;
    dl.BeginInvoke(1, 6000, AsyncCallbackImpl, dl);
}

string TakesAwhile(int data, int ms)
{

    Console.WriteLine("开始调用");
    Thread.Sleep(ms);
    Console.WriteLine("完成调用");
    string str = "测试成功";
    return str;
}

delegate string TakesAwhileDel(int data, int ms);
enum MyEnum
{
    Red,
    Bule
}