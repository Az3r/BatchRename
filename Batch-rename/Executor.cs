using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch_rename
{
    class Executor
    {
        public static FileDescriptorModel Exeucute(FileDescriptorModel target, Func<FileDescriptorModel, FileDescriptorModel> method) { return method.Invoke(target); }
    }
}
