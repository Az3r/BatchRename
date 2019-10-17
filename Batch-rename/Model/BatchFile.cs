using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
namespace BatchRename.Model
{
    /// <summary>
    /// Containing simple information of a file or folder such as name, full path, whether it exists or not
    /// </summary>
    public class BatchFile : PathInfo
    {
        private BatchFile() { FullName = string.Empty; }
        public BatchFile(string path) { FullName = path; }

        public override string GetParent()
        {
            return Path.GetDirectoryName(FullName);
        }

        public override bool Exists()
        {
            return File.Exists(FullName);
        }

        public override bool Create(bool overwrite, out string error)
        {
            error = string.Empty;
            if (overwrite) using (File.Create(FullName)) { }
            else if (Exists())
            {
                error = GetMessage(ErrorType.AlreadyExisted);
                return false;
            }
            return true;        
        }

        public override bool Delete(out string error)
        {
            error = string.Empty;
            if (!Exists())
            {
                error = GetMessage(ErrorType.NotExists);
                return false;
            }
            File.Delete(FullName);
            return true;
        }

        public override bool DeleteAll(out string error)
        {
            return Delete(out error);
        }

        public override bool Move(string destination, out string error)
        {
            error = string.Empty;
            if (File.Exists(destination))
            {
                error = GetMessage(ErrorType.AlreadyExisted);
                return false;
            }
            File.Move(FullName, destination);
            return true;
        }

        private string GetMessage(ErrorType i)
        {
            switch (i)
            {
                case ErrorType.AlreadyExisted:
                    return "File has already existed";
                case ErrorType.NotExists:
                    return "File does not exist";
                default:
                    return string.Empty;
            }
        }

        public override string GetName()
        {
            return Path.GetFileName(FullName);
        }
        public override PathInfo Clone()
        {
            return new BatchFile() { FullName = this.FullName };
        }
        private enum ErrorType
        {
            AlreadyExisted = 0,
            NotExists = 1
        }

        public override string FullName { get; set; }
    }
}