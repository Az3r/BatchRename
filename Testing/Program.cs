using System;
using System.IO;
namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "D:\\Hello\\";
            Console.WriteLine(Path.GetDirectoryName(path));
            Console.WriteLine(Path.GetFileName(path) == string.Empty) ;
            Console.WriteLine(Path.IsPathRooted("\\World"));
            Console.WriteLine(Path.Combine(path, "World"));
        }
    }
}
