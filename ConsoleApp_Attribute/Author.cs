using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Attribute
{
    [AttributeUsage(AttributeTargets.Class |AttributeTargets.Struct, AllowMultiple = true)]
    public class Author : Attribute
    {
        private string name;
        public double version;

        public Author(string name)
        {
            this.name = name;
            version = 1.0;
        }
        public string GetName()
        {
            return name;
        }
    }
}
