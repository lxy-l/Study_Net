using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public record Cat
    {
        public string Nick { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Now { get; set; }
    }
}
