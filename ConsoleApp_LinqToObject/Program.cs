using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp_LinqToObject
{
    class Program
    {
        static void Main(string[] args)
        {
            //CoutWords();
            //FindSentences();
            //QueryAString();
            //QueryWithRegEx();
            //CompareLists();
            //SortLines();
            //CSVFiles();
            //PopulateCollection();
            //SplitWithGroups();
            //JoinStrings();
            SumColumns();
        }

        /// <summary>
        /// 如何对某个词在字符串中出现的次数进行计数
        /// </summary>
        static void CoutWords()
        {
            string text = @"Historically, the world of data and the world of objects" +
        @" have not been well integrated. Programmers work in C# or Visual Basic" +
        @" and also in SQL or XQuery. On the one side are concepts such as classes," +
        @" objects, fields, inheritance, and .NET Framework APIs. On the other side" +
        @" are tables, columns, rows, nodes, and separate languages for dealing with" +
        @" them. Data types often require translation between the two worlds; there are" +
        @" different standard functions. Because the object world has no notion of query, a" +
        @" query can only be represented as a string without compile-time type checking or" +
        @" IntelliSense support in the IDE. Transferring data from SQL tables or XML trees to" +
        @" objects in memory is often tedious and error-prone.";

            string searchTerm = "data";

            //Convert the string into an array of words  
            string[] source = text.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);

            // Create the query.  Use ToLowerInvariant to match "data" and "Data"
            var matchQuery = from word in source
                             where word.ToLowerInvariant() == searchTerm.ToLowerInvariant()
                             select word;

            // Count the matches, which executes the query.  
            int wordCount = matchQuery.Count();
            Console.WriteLine("{0} occurrences(s) of the search term \"{1}\" were found.", wordCount, searchTerm);

            // Keep console window open in debug mode  
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        /// <summary>
        /// 如何查询包含一组指定词语的句子
        /// </summary>
        static void FindSentences()
        {
            string text = @"Historically, the world of data and the world of objects " +
       @"have not been well integrated. Programmers work in C# or Visual Basic " +
       @"and also in SQL or XQuery. On the one side are concepts such as classes, " +
       @"objects, fields, inheritance, and .NET Framework APIs. On the other side " +
       @"are tables, columns, rows, nodes, and separate languages for dealing with " +
       @"them. Data types often require translation between the two worlds; there are " +
       @"different standard functions. Because the object world has no notion of query, a " +
       @"query can only be represented as a string without compile-time type checking or " +
       @"IntelliSense support in the IDE. Transferring data from SQL tables or XML trees to " +
       @"objects in memory is often tedious and error-prone.";

            // Split the text block into an array of sentences.  
            string[] sentences = text.Split(new char[] { '.', '?', '!' });

            // Define the search terms. This list could also be dynamically populated at runtime.  
            string[] wordsToMatch = { "Historically", "data", "integrated" };

            // Find sentences that contain all the terms in the wordsToMatch array.  
            // Note that the number of terms to match is not specified at compile time.  
            var sentenceQuery = from sentence in sentences
                                let w = sentence.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' },
                                                        StringSplitOptions.RemoveEmptyEntries)
                                where w.Distinct().Intersect(wordsToMatch).Count() == wordsToMatch.Count()
                                select sentence;

            // Execute the query. Note that you can explicitly type  
            // the iteration variable here even though sentenceQuery  
            // was implicitly typed.
            foreach (string str in sentenceQuery)
            {
                Console.WriteLine(str);
            }

            // Keep the console window open in debug mode.  
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        /// <summary>
        /// 如何查询字符串中的字符
        /// </summary>
        static void QueryAString()
        {
            string aString = "ABCDE99F-J74-12-89A";

            // Select only those characters that are numbers  
            IEnumerable<char> stringQuery =
              from ch in aString
              where Char.IsDigit(ch)
              select ch;

            // Execute the query  
            foreach (char c in stringQuery)
                Console.Write(c + " ");

            // Call the Count method on the existing query.  
            int count = stringQuery.Count();
            Console.WriteLine("Count = {0}", count);

            // Select all characters before the first '-'  
            IEnumerable<char> stringQuery2 = aString.TakeWhile(c => c != '-');

            // Execute the second query  
            foreach (char c in stringQuery2)
                Console.Write(c);

            Console.WriteLine(System.Environment.NewLine + "Press any key to exit");
            Console.ReadKey();
        }
        /// <summary>
        /// 如何将 LINQ 查询与正则表达式合并 
        /// </summary>
        static void QueryWithRegEx()
        {
            // Modify this path as necessary so that it accesses your version of Visual Studio.  
            string startFolder = @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\";
            // One of the following paths may be more appropriate on your computer.  
            //string startFolder = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\";  

            // Take a snapshot of the file system.  
            IEnumerable<System.IO.FileInfo> fileList = GetFiles(startFolder);

            // Create the regular expression to find all things "Visual".  
            System.Text.RegularExpressions.Regex searchTerm =
                new System.Text.RegularExpressions.Regex(@"Visual (Basic|C#|C\+\+|Studio)");

            // Search the contents of each .htm file.  
            // Remove the where clause to find even more matchedValues!  
            // This query produces a list of files where a match  
            // was found, and a list of the matchedValues in that file.  
            // Note: Explicit typing of "Match" in select clause.  
            // This is required because MatchCollection is not a
            // generic IEnumerable collection.  
            var queryMatchingFiles =
                from file in fileList
                where file.Extension == ".htm"
                let fileText = System.IO.File.ReadAllText(file.FullName)
                let matches = searchTerm.Matches(fileText)
                where matches.Count > 0
                select new
                {
                    name = file.FullName,
                    matchedValues = from System.Text.RegularExpressions.Match match in matches
                                    select match.Value
                };

            // Execute the query.  
            Console.WriteLine("The term \"{0}\" was found in:", searchTerm.ToString());

            foreach (var v in queryMatchingFiles)
            {
                // Trim the path a bit, then write
                // the file name in which a match was found.  
                string s = v.name.Substring(startFolder.Length - 1);
                Console.WriteLine(s);

                // For this file, write out all the matching strings  
                foreach (var v2 in v.matchedValues)
                {
                    Console.WriteLine("  " + v2);
                }
            }

            // Keep the console window open in debug mode  
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
        // This method assumes that the application has discovery
        // permissions for all folders under the specified path.  
        static IEnumerable<System.IO.FileInfo> GetFiles(string path)
        {
            if (!System.IO.Directory.Exists(path))
                throw new System.IO.DirectoryNotFoundException();

            string[] fileNames = null;
            List<System.IO.FileInfo> files = new List<System.IO.FileInfo>();

            fileNames = System.IO.Directory.GetFiles(path, "*.*", System.IO.SearchOption.AllDirectories);
            foreach (string name in fileNames)
            {
                files.Add(new System.IO.FileInfo(name));
            }
            return files;
        }
        /// <summary>
        /// 如何查找两个列表之间的差集
        /// </summary>
        static void CompareLists()
        {
            // Create the IEnumerable data sources.  
            string[] names1 = System.IO.File.ReadAllLines(@"../../../names1.txt");
            string[] names2 = System.IO.File.ReadAllLines(@"../../../names2.txt");

            // Create the query. Note that method syntax must be used here.  
            IEnumerable<string> differenceQuery =
              names1.Except(names2);

            // Execute the query.  
            Console.WriteLine("The following lines are in names1.txt but not names2.txt");
            foreach (string s in differenceQuery)
                Console.WriteLine(s);

            // Keep the console window open in debug mode.  
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
        /// <summary>
        /// 如何按任意词或字段对文本数据进行排序或筛选
        /// </summary>
        static void SortLines()
        {
            // Create an IEnumerable data source  
            string[] scores = System.IO.File.ReadAllLines(@"../../../scores.csv");

            // Change this to any value from 0 to 4.  
            int sortField = 1;

            Console.WriteLine("Sorted highest to lowest by field [{0}]:", sortField);

            // Demonstrates how to return query from a method.  
            // The query is executed here.  
            foreach (string str in RunQuery(scores, sortField))
            {
                Console.WriteLine(str);
            }

            // Keep the console window open in debug mode.  
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
        static IEnumerable<string> RunQuery(IEnumerable<string> source, int num)
        {
            // Split the string and sort on field[num]  
            var scoreQuery = from line in source
                             let fields = line.Split(',')
                             orderby fields[num] descending
                             select line;

            return scoreQuery;
        }
        /// <summary>
        /// 如何重新排列带分隔符的文件的字段
        /// </summary>
        static void CSVFiles()
        {
            // Create the IEnumerable data source  
            string[] lines = System.IO.File.ReadAllLines(@"../../../spreadsheet1.csv");

            // Create the query. Put field 2 first, then  
            // reverse and combine fields 0 and 1 from the old field  
            IEnumerable<string> query =
                from line in lines
                let x = line.Split(',')
                orderby x[2]
                select x[2] + ", " + (x[1] + " " + x[0]);

            // Execute the query and write out the new file. Note that WriteAllLines  
            // takes a string[], so ToArray is called on the query.  
            System.IO.File.WriteAllLines(@"../../../spreadsheet2.csv", query.ToArray());

            Console.WriteLine("Spreadsheet2.csv written to disk. Press any key to exit");
            Console.ReadKey();
        }
        /// <summary>
        /// 如何从多个源填充对象集合 
        /// </summary>
        static void PopulateCollection()
        {
            // These data files are defined in How to join content from
            // dissimilar files (LINQ).

            // Each line of names.csv consists of a last name, a first name, and an
            // ID number, separated by commas. For example, Omelchenko,Svetlana,111
            string[] names = System.IO.File.ReadAllLines(@"../../../names.csv");

            // Each line of scores.csv consists of an ID number and four test
            // scores, separated by commas. For example, 111, 97, 92, 81, 60
            string[] scores = System.IO.File.ReadAllLines(@"../../../scores.csv");

            // Merge the data sources using a named type.
            // var could be used instead of an explicit type. Note the dynamic
            // creation of a list of ints for the ExamScores member. The first item
            // is skipped in the split string because it is the student ID,
            // not an exam score.
            IEnumerable<Student> queryNamesScores =
                from nameLine in names
                let splitName = nameLine.Split(',')
                from scoreLine in scores
                let splitScoreLine = scoreLine.Split(',')
                where Convert.ToInt32(splitName[2]) == Convert.ToInt32(splitScoreLine[0])
                select new Student()
                {
                    FirstName = splitName[0],
                    LastName = splitName[1],
                    ID = Convert.ToInt32(splitName[2]),
                    ExamScores = (from scoreAsText in splitScoreLine.Skip(1)
                                  select Convert.ToInt32(scoreAsText)).
                                  ToList()
                };

            // Optional. Store the newly created student objects in memory
            // for faster access in future queries. This could be useful with
            // very large data files.
            List<Student> students = queryNamesScores.ToList();

            // Display each student's name and exam score average.
            foreach (var student in students)
            {
                Console.WriteLine("The average score of {0} {1} is {2}.",
                    student.FirstName, student.LastName,
                    student.ExamScores.Average());
            }

            //Keep console window open in debug mode
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
        /// <summary>
        /// 如何使用组将一个文件拆分成多个文件
        /// </summary>
        static void SplitWithGroups()
        {
            string[] fileA = System.IO.File.ReadAllLines(@"../../../names1.txt");
            string[] fileB = System.IO.File.ReadAllLines(@"../../../names2.txt");

            // Concatenate and remove duplicate names based on  
            // default string comparer  
            var mergeQuery = fileA.Union(fileB);

            // Group the names by the first letter in the last name.  
            var groupQuery = from name in mergeQuery
                             let n = name.Split(',')
                             group name by n[0][0] into g
                             orderby g.Key
                             select g;

            // Create a new file for each group that was created  
            // Note that nested foreach loops are required to access  
            // individual items with each group.  
            foreach (var g in groupQuery)
            {
                // Create the new file name.  
                string fileName = @"../../../testFile_" + g.Key + ".txt";

                // Output to display.  
                Console.WriteLine(g.Key);

                // Write file.  
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName))
                {
                    foreach (var item in g)
                    {
                        sw.WriteLine(item);
                        // Output to console for example purposes.  
                        Console.WriteLine("   {0}", item);
                    }
                }
            }
            // Keep console window open in debug mode.  
            Console.WriteLine("Files have been written. Press any key to exit");
            Console.ReadKey();
        }
        /// <summary>
        /// 如何联接不同文件的内容
        /// </summary>
        static void JoinStrings()
        {
            // Join content from dissimilar files that contain  
            // related information. File names.csv contains the student  
            // name plus an ID number. File scores.csv contains the ID
            // and a set of four test scores. The following query joins  
            // the scores to the student names by using ID as a  
            // matching key.  

            string[] names = System.IO.File.ReadAllLines(@"../../../names.csv");
            string[] scores = System.IO.File.ReadAllLines(@"../../../scores.csv");

            // Name:    Last[0],       First[1],  ID[2]  
            //          Omelchenko,    Svetlana,  11  
            // Score:   StudentID[0],  Exam1[1]   Exam2[2],  Exam3[3],  Exam4[4]  
            //          111,           97,        92,        81,        60  

            // This query joins two dissimilar spreadsheets based on common ID value.  
            // Multiple from clauses are used instead of a join clause  
            // in order to store results of id.Split.  
            IEnumerable<string> scoreQuery1 =
                from name in names
                let nameFields = name.Split(',')
                from id in scores
                let scoreFields = id.Split(',')
                where Convert.ToInt32(nameFields[2]) == Convert.ToInt32(scoreFields[0])
                select nameFields[0] + "," + scoreFields[1] + "," + scoreFields[2]
                       + "," + scoreFields[3] + "," + scoreFields[4];

            // Pass a query variable to a method and execute it  
            // in the method. The query itself is unchanged.  
            OutputQueryResults(scoreQuery1, "Merge two spreadsheets:");

            // Keep console window open in debug mode.  
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
        static void OutputQueryResults(IEnumerable<string> query, string message)
        {
            Console.WriteLine(System.Environment.NewLine + message);
            foreach (string item in query)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("{0} total names in list", query.Count());
        }
        /// <summary>
        /// 如何在 CSV 文本文件中计算列值
        /// </summary>
        static void SumColumns()
        {
            string[] lines = System.IO.File.ReadAllLines(@"../../../scores.csv");

            // Specifies the column to compute.  
            int exam = 3;

            // Spreadsheet format:  
            // Student ID    Exam#1  Exam#2  Exam#3  Exam#4  
            // 111,          97,     92,     81,     60  

            // Add one to exam to skip over the first column,  
            // which holds the student ID.  
            SingleColumn(lines, exam + 1);
            Console.WriteLine();
            MultiColumns(lines);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
        static void SingleColumn(IEnumerable<string> strs, int examNum)
        {
            Console.WriteLine("Single Column Query:");

            // Parameter examNum specifies the column to
            // run the calculations on. This value could be  
            // passed in dynamically at runtime.

            // Variable columnQuery is an IEnumerable<int>.  
            // The following query performs two steps:  
            // 1) use Split to break each row (a string) into an array
            //    of strings,
            // 2) convert the element at position examNum to an int  
            //    and select it.  
            var columnQuery =
                from line in strs
                let elements = line.Split(',')
                select Convert.ToInt32(elements[examNum]);

            // Execute the query and cache the results to improve  
            // performance. This is helpful only with very large files.  
            var results = columnQuery.ToList();

            // Perform aggregate calculations Average, Max, and  
            // Min on the column specified by examNum.  
            double average = results.Average();
            int max = results.Max();
            int min = results.Min();

            Console.WriteLine("Exam #{0}: Average:{1:##.##} High Score:{2} Low Score:{3}",
                     examNum, average, max, min);
        }
        static void MultiColumns(IEnumerable<string> strs)
        {
            Console.WriteLine("Multi Column Query:");

            // Create a query, multiColQuery. Explicit typing is used  
            // to make clear that, when executed, multiColQuery produces
            // nested sequences. However, you get the same results by  
            // using 'var'.  

            // The multiColQuery query performs the following steps:  
            // 1) use Split to break each row (a string) into an array
            //    of strings,
            // 2) use Skip to skip the "Student ID" column, and store the
            //    rest of the row in scores.  
            // 3) convert each score in the current row from a string to  
            //    an int, and select that entire sequence as one row
            //    in the results.  
            IEnumerable<IEnumerable<int>> multiColQuery =
                from line in strs
                let elements = line.Split(',')
                let scores = elements.Skip(1)
                select (from str in scores
                        select Convert.ToInt32(str));

            // Execute the query and cache the results to improve  
            // performance.
            // ToArray could be used instead of ToList.  
            var results = multiColQuery.ToList();

            // Find out how many columns you have in results.  
            int columnCount = results[0].Count();

            // Perform aggregate calculations Average, Max, and  
            // Min on each column.
            // Perform one iteration of the loop for each column
            // of scores.  
            // You can use a for loop instead of a foreach loop
            // because you already executed the multiColQuery
            // query by calling ToList.  
            for (int column = 0; column < columnCount; column++)
            {
                var results2 = from row in results
                               select row.ElementAt(column);
                double average = results2.Average();
                int max = results2.Max();
                int min = results2.Min();

                // Add one to column because the first exam is Exam #1,  
                // not Exam #0.  
                Console.WriteLine("Exam #{0} Average: {1:##.##} High Score: {2} Low Score: {3}",
                              column + 1, average, max, min);
            }
        }
        class Student
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int ID { get; set; }
            public List<int> ExamScores { get; set; }
        }
    }
}
