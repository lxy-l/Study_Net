using System;

namespace ConsoleApp_Serialization
{
    /*
     序列化是指将对象转换成字节流，从而存储对象或将对象传输到内存、数据库或文件的过程。 它的主要用途是保存对象的状态，以便能够在需要时重新创建对象。 反向过程称为“反序列化”。

    通过序列化，开发人员可以保存对象的状态，并能在需要时重新创建对象，同时还能存储对象和交换数据。 通过序列化，开发人员可以执行如下操作：
                使用 Web 服务将对象发送到远程应用程序
                将对象从一个域传递到另一个域
                将对象通过防火墙传递为 JSON 或 XML 字符串
                跨应用程序维护安全或用户特定的信息
     */
    class Program
    {
        static void Main(string[] args)
        {
            WriteXML(); ReadXML();
        }

        public static void WriteXML()
        {
            Book overview = new Book();
            overview.title = "Serialization Overview";
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(Book));

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml";
            System.IO.FileStream file = System.IO.File.Create(path);

            writer.Serialize(file, overview);
            file.Close();
        }
        public static void ReadXML()
        {
            // First write something so that there is something to read ...  
            var b = new Book { title = "Serialization Overview" };
            var writer = new System.Xml.Serialization.XmlSerializer(typeof(Book));
            var wfile = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml");
            writer.Serialize(wfile, b);
            wfile.Close();

            // Now we can read the serialized book ...  
            System.Xml.Serialization.XmlSerializer reader =
                new System.Xml.Serialization.XmlSerializer(typeof(Book));
            System.IO.StreamReader file = new System.IO.StreamReader(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml");
            Book overview = (Book)reader.Deserialize(file);
            file.Close();

            Console.WriteLine(overview.title);

        }
    }


    public class Book
    {
        public String title;
    }


}
