using System.Collections.Generic;

namespace ConsoleApp_Collections
{
    /*
     *  可以使用 System.Collections.Generic 命名空间中的某个类来创建泛型集合。 当集合中的所有项都具有相同的数据类型时，泛型集合会非常有用。 泛型集合通过仅允许添加所需的数据类型，强制实施强类型化
     
     */
    public class Generic
    {
        Dictionary<int, string> keyValuePairs = new Dictionary<int, string>();
        List<string> list = new List<string>();
        Queue<string> queue = new Queue<string>();
        SortedList<int, string> sortedlist = new SortedList<int, string>();
        Stack<string> stack = new Stack<string>();
        
    }
}
