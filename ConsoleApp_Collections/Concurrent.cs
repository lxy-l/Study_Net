using System.Collections;

namespace ConsoleApp_Collections
{
    /*
     *  System.Collections 命名空间中的类不会将元素作为特别类型化的对象存储，而是作为 Object 类型的对象存储。
        只要多个线程同时访问集合，就应使用 System.Collections.Concurrent 命名空间中的类，而不是 System.Collections.Generic 和 System.Collections 命名空间中的相应类型。
       
     */
    public class Concurrent
    {
        ArrayList arrayList = new ArrayList();
        Hashtable hashtable = new Hashtable();
        Queue queue = new Queue();
        Stack stack = new Stack();
    }
}
