using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ConsoleApp_Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            ToStudentAndTeacher();
            ToXML();
            ToArea();

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
}
