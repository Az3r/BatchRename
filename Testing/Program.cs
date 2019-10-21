using System;
using System.IO;
using System.Collections.Generic;
namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "\\Hello\\World";
            Console.WriteLine(Path.GetDirectoryName(Path.GetDirectoryName(path)));
            Console.WriteLine(Directory.GetParent(path));
            Console.WriteLine(Path.GetFileName(path));
            Console.WriteLine(Path.IsPathRooted("\\World"));
            Console.WriteLine(Path.Combine(path, "World"));

            //TestEnum foo = TestEnum.foo;


            //Dictionary<string, TestEnum> map = new Dictionary<string, TestEnum>();
            //map.Add("foo", foo);
            //map["foo"] = foo;
            //TestEnum[] arr = (TestEnum[]) Enum.GetValues(typeof(TestEnum));
            //foreach (TestEnum item in arr)
            //    Console.WriteLine((int)item);
        }
        enum TestEnum
        {
            foo = 1,
            bar = 2
        }
    }
    public class Foo
    {
        public const string Name = "Foo";
    }
    public class Bar : Foo
    {
        public new const string Name = "Bar";
    }
}
