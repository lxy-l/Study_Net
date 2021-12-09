using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_OOP.Class
{
    public class Me : Base
    {
        public override string Name => "Me";


        public override void He(string name)
        {
            Console.WriteLine("Fuck"+name);
        }

        public override int Fuck(int i)
        {
            return base.Fuck(i+1);
        }

        public void He(string m,int q)
        {
            Console.WriteLine(m);
        }
    }
}
