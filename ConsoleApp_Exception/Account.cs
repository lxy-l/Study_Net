using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp_Exception
{
    public sealed class Account
    {
        private static decimal  balance=6;
        private static readonly object sync = new object();
        public static void Withdraw(decimal amount)
        {
            lock (sync)
            {
                if (amount > balance)
                {
                    throw new Exception(
                        "Insufficient funds");
                }
                balance -= amount;
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId+":"+balance);
            }
        }
    }
}
