using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ConsoleApp_Lambda
{
    class Program
    {
        /**
         表达式 lambda:(input-parameters) => expression
         语句 lambda:(input-parameters) => { <sequence-of-statements> }
         任何 Lambda 表达式都可以转换为委托类型。 
         */
        static void Main(string[] args)
        {
            Func<int, int> square = x => x * x;
            Console.WriteLine(square(5));
            //Console.WriteLine(Math.BigMul(5,5));
            System.Linq.Expressions.Expression<Func<int, int>> e = x => x * x;
            Console.WriteLine(e);

            int[] numbers = { 2, 3, 4, 5 };
            var squaredNumbers = numbers.Select(x => x * x);
            Console.WriteLine(squaredNumbers+"->"+string.Join(",", squaredNumbers));

            Action<string> greet = name =>
            {
                string greeting = $"Hello {name}!";
                Console.WriteLine(greeting);
            };
            greet("World");

            Func<(int, int, int), (int, int, int)> doubleThem = ns => (2 * ns.Item1, 2 * ns.Item2, 2 * ns.Item3);
            var values = (2, 3, 4);
            var doubledNumbers = doubleThem(values);
            Console.WriteLine($"The set {values} doubled: {doubledNumbers}");

            Func<int, bool> equalsFive = x => x == 5;
            bool result = equalsFive(4);
            Console.Error.WriteLine(result);   // False


            Expression<Func<int, int>> addFive = (num) => num + 5;

            if (addFive.NodeType == ExpressionType.Lambda)
            {
                var lambdaExp = (LambdaExpression)addFive;

                var parameter = lambdaExp.Parameters.First();

                Console.WriteLine(parameter.Name);
                Console.WriteLine(parameter.Type);
            }

        }
    }
}
