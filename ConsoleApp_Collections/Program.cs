using System;
using System.Collections.Generic;

namespace ConsoleApp_Collections
{
    /*
     * 
     * 可以通过实现 IEnumerable<T> 或 IEnumerable 接口来定义集合。
     * GetEnumerator 方法返回 ColorEnumerator 类的一个实例。 ColorEnumerator 实现 IEnumerator 接口，此操作需要实现 Current 属性、MoveNext 方法以及 Reset 方法。
     * 
        迭代器 用于对集合执行自定义迭代。 迭代器可以是一种方法，或是一个 get 访问器。 迭代器使用 yield return 语句返回集合的每一个元素，每次返回一个元素。
        通过使用 foreach 语句调用迭代器。 foreach 循环的每次迭代都会调用迭代器。 迭代器中到达 yield return 语句时，会返回一个表达式，并保留当前在代码中的位置。 下次调用迭代器时，将从该位置重新开始执行。
     
     */
    class Program
    {
        static void Main(string[] args)
        {
            //ListColors();
            ListEvenNumbers();
            Console.WriteLine("Hello World!");
        }

        private static void ListColors()
        {
            var colors = new AllColors();

            foreach (Color theColor in colors)
            {
                Console.Write(theColor.Name + " ");
            }
            Console.WriteLine();
        }
        public class AllColors : System.Collections.IEnumerable
        {
            Color[] _colors =
            {
                new Color() { Name = "red" },
                new Color() { Name = "blue" },
                new Color() { Name = "green" }
            };

            public System.Collections.IEnumerator GetEnumerator()
            {
                //也可用默认方法 return _colors.GetEnumerator()
                return new ColorEnumerator(_colors);
            }
            private class ColorEnumerator : System.Collections.IEnumerator
            {
                private Color[] _colors;
                private int _position = -1;

                public ColorEnumerator(Color[] colors)
                {
                    _colors = colors;
                }

                object System.Collections.IEnumerator.Current
                {
                    //如果MoveNext返回true，返回这个数组这个的值
                    get
                    {
                        return _colors[_position];
                    }
                }

                bool System.Collections.IEnumerator.MoveNext()
                {
                    //foreach遍历时，MoveNext判断下标是否越界
                    _position++;
                    return (_position < _colors.Length);
                }

                void System.Collections.IEnumerator.Reset()
                {
                    _position = -1;
                }
            }
        }
        public class Color
        {
            public string Name { get; set; }
        }

        private static void ListEvenNumbers()
        {
            IEnumerable<int> list = EvenSequence(5, 18);
            foreach (int number in list)
            {
                Console.Write(number.ToString() + " ");
            }
            Console.WriteLine();
        }

        private static IEnumerable<int> EvenSequence(int firstNumber, int lastNumber)
        {
            for (var number = firstNumber; number <= lastNumber; number++)
            {
                if (number % 2 == 0)
                {
                    yield return number;
                }
            }
        }

    }
}
