using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ConsoleApp_Linq
{
    /*
     Skip	跳过序列中指定位置之前的元素。
     SkipWhile	基于谓词函数跳过元素，直到元素不符合条件。
     Take	获取序列中指定位置之前的元素。
     TakeWhile	基于谓词函数获取元素，直到元素不符合条件。
     */
    class Program
    {
        static void Main(string[] args)
        {

            //ToStudentAndTeacher();
            //ToXML();
            //ToArea();

            //集合运算
            //Set.Distinct();
            //Set.Except();
            //Set.Intersect();
            //Set.Union();

            //投影运算
            //SelectVsSelectMany();

            //连接运算
            //ExampleJoin();
            //ExampleGroupJoin();

            //分组
            //GroupBy();


        }

        public static void ToXML()
        {
            List<Student> students = new List<Student>()
        {
            new Student {First="Svetlana", Last="Omelchenko", ID=111, Scores = new List<int>{97, 92, 81, 60}},
            new Student {First="Claire", Last="O’Donnell", ID=112, Scores = new List<int>{75, 84, 91, 39}},
            new Student {First="Sven", Last="Mortensen", ID=113, Scores = new List<int>{88, 94, 65, 91}},
        };

            // Create the query.
            var studentsToXML = new XElement("Root",//创建根节点
                from student in students//数据源
                let scores = string.Join(",", student.Scores)//定义变量，拼接成绩
                select new XElement("student",//创建学生对象xml
                           new XElement("First", student.First),//学生信息
                           new XElement("Last", student.Last),
                           new XElement("Scores", scores)
                        ) // end "student"
                    ); // end "Root"

            // Execute the query.
            Console.WriteLine(studentsToXML);
        }

        public static void ToArea()
        {
            // Data source.
            double[] radii = { 1, 2, 3 };

            // Query.
            IEnumerable<string> query =
                from rad in radii
                select $"Area = {rad * rad * Math.PI:F2}";

            // Query execution.
            foreach (string s in query)
                Console.WriteLine(s);

        }

        public static void ToStudentAndTeacher()
        {
            // Create the first data source.
            List<Student> students = new List<Student>()
            {
                new Student { First="Svetlana",
                    Last="Omelchenko",
                    ID=111,
                    Street="123 Main Street",
                    City="Seattle",
                    Scores= new List<int> { 97, 92, 81, 60 } },
                new Student { First="Claire",
                    Last="O’Donnell",
                    ID=112,
                    Street="124 Main Street",
                    City="Redmond",
                    Scores= new List<int> { 75, 84, 91, 39 } },
                new Student { First="Sven",
                    Last="Mortensen",
                    ID=113,
                    Street="125 Main Street",
                    City="Lake City",
                    Scores= new List<int> { 88, 94, 65, 91 } },
            };

            // Create the second data source.
            List<Teacher> teachers = new List<Teacher>()
            {
                new Teacher { First="Ann", Last="Beebe", ID=945, City="Seattle" },
                new Teacher { First="Alex", Last="Robinson", ID=956, City="Redmond" },
                new Teacher { First="Michiyo", Last="Sato", ID=972, City="Tacoma" }
            };

            // Create the query.
            var peopleInSeattle = (from student in students
                                   where student.City == "Seattle"
                                   select student.Last)
                        .Concat(from teacher in teachers//连接两个数据源
                                where teacher.City == "Seattle"
                                select teacher.Last);

            Console.WriteLine("The following students and teachers live in Seattle:");
            // Execute the query.
            foreach (var person in peopleInSeattle)
            {
                Console.WriteLine(person);
            }
        }

        public static void SelectVsSelectMany()
        {
            List<Bouquet> bouquets = new List<Bouquet>()
            {
                new Bouquet { Flowers = new List<string> { "sunflower", "daisy", "daffodil", "larkspur" } },
                new Bouquet { Flowers = new List<string> { "tulip", "rose", "orchid" } },
                new Bouquet { Flowers = new List<string> { "gladiolis", "lily", "snapdragon", "aster", "protea" } },
                new Bouquet { Flowers = new List<string> { "larkspur", "lilac", "iris", "dahlia" } }
            };

            // *********** Select ***********
            IEnumerable<List<string>> query1 = bouquets.Select(bq => bq.Flowers);// Select() 为每个源值生成一个结果值

            // ********* SelectMany *********  
            IEnumerable<string> query2 = bouquets.SelectMany(bq => bq.Flowers);//SelectMany() 生成单个总体结果，其中包含来自每个源值的串联子集合
            //作为参数传递到 SelectMany() 的转换函数必须为每个源值返回一个可枚举值序列。 然后，SelectMany() 串联这些可枚举序列，以创建一个大的序列。

            Console.WriteLine("Results by using Select():");
            // Note the extra foreach loop here.  
            foreach (IEnumerable<string> collection in query1)
                foreach (string item in collection)
                    Console.WriteLine(item);

            Console.WriteLine("\nResults by using SelectMany():");
            foreach (string item in query2)
                Console.WriteLine(item);
        }

        public static void ExampleJoin()
        {
            List<Product> products = new List<Product>
            {
                new Product { Name = "Cola", CategoryId = 0 },
                new Product { Name = "Tea", CategoryId = 0 },
                new Product { Name = "Apple", CategoryId = 1 },
                new Product { Name = "Kiwi", CategoryId = 1 },
                new Product { Name = "Carrot", CategoryId = 2 },
            };

            List<Category> categories = new List<Category>
            {
                new Category { Id = 0, CategoryName = "Beverage" },
                new Category { Id = 1, CategoryName = "Fruit" },
                new Category { Id = 2, CategoryName = "Vegetable" }
            };

            // Join products and categories based on CategoryId
            var query = from product in products
                        join category in categories on product.CategoryId equals category.Id
                        select new { product.Name, category.CategoryName };

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Name} - {item.CategoryName}");
            }

            // This code produces the following output:
            //
            // Cola - Beverage
            // Tea - Beverage
            // Apple - Fruit
            // Kiwi - Fruit
            // Carrot - Vegetable
        }

        public static void ExampleGroupJoin()
        {
            List<Product> products = new List<Product>
            {
                new Product { Name = "Cola", CategoryId = 0 },
                new Product { Name = "Tea", CategoryId = 0 },
                new Product { Name = "Apple", CategoryId = 1 },
                new Product { Name = "Kiwi", CategoryId = 1 },
                new Product { Name = "Carrot", CategoryId = 2 },
            };

            List<Category> categories = new List<Category>
            {
                new Category { Id = 0, CategoryName = "Beverage" },
                new Category { Id = 1, CategoryName = "Fruit" },
                new Category { Id = 2, CategoryName = "Vegetable" }
            };

            // Join categories and product based on CategoryId and grouping result
            var productGroups = from category in categories
                                join product in products on category.Id equals product.CategoryId into productGroup
                                select productGroup;

            foreach (IEnumerable<Product> productGroup in productGroups)
            {
                Console.WriteLine("Group");
                foreach (Product product in productGroup)
                {
                    Console.WriteLine($"{product.Name,8}");
                }
            }
        }

        public static void GroupBy()
        {
            List<int> numbers = new List<int>() { 35, 44, 200, 84, 3987, 4, 199, 329, 446, 208 };

            IEnumerable<IGrouping<int, int>> query = from number in numbers
                                                     group number by number % 2;//奇偶分组

            foreach (var group in query)
            {
                Console.WriteLine(group.Key == 0 ? "\nEven numbers:" : "\nOdd numbers:");
                foreach (int i in group)
                    Console.WriteLine(i);
            }
        }

        class Student
        {
            public string First { get; set; }
            public string Last { get; set; }
            public int ID { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public List<int> Scores;
        }

        class Teacher
        {
            public string First { get; set; }
            public string Last { get; set; }
            public int ID { get; set; }
            public string City { get; set; }
        }

        class Bouquet
        {
            public List<string> Flowers { get; set; }
        }

        class Product
        {
            public string Name { get; set; }
            public int CategoryId { get; set; }
        }

        class Category
        {
            public int Id { get; set; }
            public string CategoryName { get; set; }
        }

    }
}
