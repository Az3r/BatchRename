using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Batch_rename
{
    class FileDescriptorModel
    {
        public FileDescriptorModel() { }
        public string Name { get => name; set => name = value; }
        public string FullPath { get => path; set => path = value; }


        private string name;
        private string path;
    }
}
