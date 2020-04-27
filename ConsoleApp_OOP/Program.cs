using System;
using System.Buffers;

namespace ConsoleApp_OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            SampleClass sampleObject = new SampleClass();
            sampleObject.Sample = "Sample String";
            sampleObject.sampleMethod(1);
            SampleGeneric<string> asdf = new SampleGeneric<string>();
            asdf.Field = "Sample string";
            Container.Nested nestedInstance = new Container.Nested();
        }
    }
    struct SampleStruct
    {
    }
    class SampleClass
    {
        private string _sample;
        public static string SampleString = "Sample String";
        public SampleClass()
        {
            // Add code here
        }
        public string Sample
        {
            // Return the value stored in a field.
            get => _sample;
            // Store the value in the field.
            set => _sample = value;
        }
        public int sampleMethod(string sampleParam) { return 0; }
        public void sampleMethod() { Console.WriteLine("Ok"); }
        public int sampleMethod(int sampleParam) { return sampleParam; }
        public void SampleDelegate()
        {
            SampleDelegate sd = sampleMethod;
            sd();
        }

    }

    public class Container
    {
        public class Nested
        {
            // Add code here.
        }
    }

    class DerivedClass : SampleClass { }
    public abstract class B { }
    public sealed class A { }
    interface ISampleInterface
    {
        void doSomething();
    }

    class C : ISampleInterface
    {
        void ISampleInterface.doSomething()
        {
            // Method implementation.
        }
    }
    public class SampleGeneric<T>
    {
        public T Field;
    }

    public delegate void SampleDelegate();
}
