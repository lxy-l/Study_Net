using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp_Thread
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 线程Thread/ThreadPool
            Console.WriteLine("执行线程");
            Thread th = new Thread((objParam) =>{
                Console.WriteLine("线程启动，执行匿名方法，有无参数{0}", objParam != null);
            });
            th.IsBackground = true;
            object objP = new object();
            th.Start(objP);

            //线程池
            //线程池初始化执行方法必须带一个object参数，接受到的值是系统默认NULL（不明），所以初始化完成自动调用
            Console.WriteLine("执行线程池");
            ThreadPool.QueueUserWorkItem((objparam) =>
            {
                Console.WriteLine("线程池加入的匿名方法被执行。");
            });

            #endregion

            #region 并行循环
            int result = 0;
            int result2 = 0;
            object obj = new object();
            int lockResult = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //并行循环
            //并行应该用于一次执行多个相同任务，或计算结果和循环的游标没有关系只和执行次数有关系的计算
            Parallel.For(0, 100, (i) =>
            {
                result = result + 2;
                //lock只能lock引用类型，利用引用对象的地址唯一作为锁，实现lock中的代码一次只能一个线程访问
                //lock让lock里的代码在并行时变为串行，尽量不要在parallel中用lock（lock内的操作耗时小，lock外操作耗时大时，并行还是起作用）
                lock (obj)
                {
                    lockResult = lockResult + 2;
                    Thread.Sleep(100);
                    Console.WriteLine("i={0},lockResult={1}", i, lockResult);
                }
                Console.WriteLine("i={0},result={1}", i, result);
            });
            stopwatch.Stop();

            Console.WriteLine("耗时:"+stopwatch.Elapsed);
           
            stopwatch.Restart();
            for (int i = 0; i < 1000; i++)
            {
                result2 = result2 + 2;
            }
            stopwatch.Stop();

            Console.WriteLine("耗时:" + stopwatch.Elapsed);

            Console.WriteLine("result:"+result);
            Console.WriteLine("result2:"+result2);
            Console.WriteLine("lockResult:" + lockResult);


            Action<int, ParallelLoopState> body = (i,b) => 
            {
                if (i==2)
                {
                    b.Stop();
                }
            };

            Parallel.For(0, 10, body);

            #endregion

            #region Task任务
            //任务
            Task.Run(() =>
            {
                Thread.Sleep(200);
                Console.WriteLine("Task启动执行匿名方法");
            });
            Console.WriteLine("Task默认不阻塞");

            //获取Task.Result会造成阻塞等待task执行
            int r = Task.Run(() =>
            {
                Console.WriteLine("Task启动执行匿名方法并返回值");
                Thread.Sleep(1000);
                return 5;
            }).Result;
            Console.WriteLine("返回值是{0}", r);
            #endregion

            #region 异步方法

            //DateTime pbgtime = DateTime.Now;
            //for (int i = 0; i < 50; i++)
            //{
            //    MethodC(pbgtime, i);
            //    Console.WriteLine("普通方法{0}调用完成", i);
            //}

            //DateTime abgtime = DateTime.Now;
            //for (int i = 0; i < 50; i++)
            //{
            //    MethodA(abgtime, i).ConfigureAwait(false);
            //    Console.WriteLine("异步方法{0}调用完成", i);
            //}
            #endregion
        }

        //异步方法
        public static async Task<int> MethodA(DateTime bgtime, int i)
        {
            int r = await Task.Run(() =>
            {
                Console.WriteLine("异步方法{0}Task被执行", i);
                Thread.Sleep(100);
                return i * 2;
            });
            Console.WriteLine("异步方法{0}执行完毕，结果{1}", i, r);

            if (i == 49)
            {
                Console.WriteLine("用时{0}", (DateTime.Now - bgtime).TotalMilliseconds);
            }
            return r;
        }
        //普通方法
        public static int MethodC(DateTime bgtime, int i)
        {
            int r = Task.Run(() =>
            {
                Console.WriteLine("普通多线程方法{0}Task被执行", i);
                Thread.Sleep(100);
                return i * 2;
            }).Result;
            Console.WriteLine("普通方法{0}执行完毕，结果{1}", i, r);

            if (i == 49)
            {
                Console.WriteLine("用时{0}", (DateTime.Now - bgtime).TotalMilliseconds);
            }
            return r;
        }
    }
}
