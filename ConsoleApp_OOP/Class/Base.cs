using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_OOP.Class
{
    public abstract class Base
    {
        public abstract string Name { get; }

        public string Description { get; }

        public abstract void He(string name);

        public void Hello()
        {
            Console.WriteLine("Fuck！");
        }


        public virtual int Fuck(int i)
        {
            return i;
        }
    }
}
