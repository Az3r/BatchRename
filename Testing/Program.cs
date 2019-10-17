using System;
using System.IO;
namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "\\\\\\";
            Console.WriteLine(Path.GetDirectoryName(Path.GetDirectoryName(path)));
            Console.WriteLine(Directory.GetParent(path));
            Console.WriteLine(Path.GetFileName(path) == string.Empty) ;
            Console.WriteLine(Path.IsPathRooted("\\World"));
            Console.WriteLine(Path.Combine(path, "World"));
            Console.WriteLine(nameof(TestEnum.ABD));
        }
        enum TestEnum
        {
            ABD = 1
        }
    }
}
