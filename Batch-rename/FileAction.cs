using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch_rename
{
    class FileAction
    {
        public FileAction() {}
        public delegate FileInfo HandlerDelegate(FileInfo target);
        public string Name { get; set; }
        public HandlerDelegate Handler { get; set; }
    }
}
