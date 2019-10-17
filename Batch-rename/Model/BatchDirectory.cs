using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace BatchRename.Model
{
    public class BatchDirectory : PathInfo
    {
        public BatchDirectory() { FullName = string.Empty; }
        public BatchDirectory(string path) { FullName = path; }

        public override bool Create(bool overwrite, out string error)
        {
            error = string.Empty;
            if (overwrite)
            {
                Directory.Delete(FullName, true);
            }
            else if (Exists())
            {
                error = GetMessage(ErrorType.AlreadyExisted);
                return false;
            }
            Directory.CreateDirectory(FullName);
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
            else if (!IsEmpty())
            {
                error = GetMessage(ErrorType.DirectoryNotEmpty);
                return false;
            }
            Directory.Delete(FullName);
            return true;
        }

        public override bool DeleteAll(out string error)
        {
            error = string.Empty;
            if (!Exists())
            {
                error = GetMessage(ErrorType.NotExists);
                return false;
            }
            Directory.Delete(FullName, true);
            return true;
        }

        public override bool Move(string destination, out string error)
        {
            error = string.Empty;

            if (Directory.Exists(destination))
            {
                error = GetMessage(ErrorType.AlreadyExisted);
                return false;
            }
            else if(!Exists())
            {

                error = GetMessage(ErrorType.NotExists);
                return false;
            }
            Directory.Move(FullName, destination);
            return true;
        }
        public bool IsEmpty()
        {
            return !Directory.EnumerateFileSystemEntries(FullName).Any();
        }
        public override string GetParent()
        {
            return Path.GetDirectoryName(Path.GetDirectoryName(FullName));
        }

        public override bool Exists()
        {
            return Directory.Exists(FullName);
        }
        public override string GetName()
        {
            return Directory.GetParent(FullName).Name;
        }

        public override PathInfo Clone()
        {
            return new BatchDirectory() { FullName = this.FullName };
        }
        private string GetMessage(ErrorType i)
        {
            switch (i)
            {
                case ErrorType.AlreadyExisted:
                    return "Directory has already existed";
                case ErrorType.NotExists:
                    return "Directory does not exist";
                case ErrorType.DirectoryNotEmpty:
                    return "Directory is not empty";
                default:
                    return string.Empty;
            }
        }
        private enum ErrorType : int
        {
            AlreadyExisted = 0,
            NotExists = 1,
            DirectoryNotEmpty = 2
        }
        public override string FullName
        {
            get
            {
                return sPath;
            }
            set
            {
                sPath = value.Insert(value.Length - 1, "\\");
            }
        }

        private string sPath;
    }
}
