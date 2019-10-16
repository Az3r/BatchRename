using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace BatchRename.Model
{
    public class BatchDirectory : IBatchPath
    {
        public BatchDirectory() { FullPath = string.Empty; }
        public BatchDirectory(string path) { FullPath = path; }

        public bool Create(bool overwrite, out string error)
        {
            error = string.Empty;
            if (overwrite)
            {
                Directory.Delete(FullPath, true);
            }
            else if (Exists())
            {
                error = GetMessage(ErrorType.AlreadyExisted);
                return false;
            }
            Directory.CreateDirectory(FullPath);
            return true;
        }

        public bool Delete(out string error)
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
            Directory.Delete(FullPath);
            return true;
        }

        public bool DeleteAll(out string error)
        {
            error = string.Empty;
            if (!Exists())
            {
                error = GetMessage(ErrorType.NotExists);
                return false;
            }
            Directory.Delete(FullPath, true);
            return true;
        }

        public bool Move(string destination, out string error)
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
            Directory.Move(FullPath, destination);
            return true;
        }
        public bool IsEmpty()
        {
            return !Directory.EnumerateFileSystemEntries(FullPath).Any();
        }
        public string GetParent()
        {
            return Path.GetDirectoryName(Path.GetDirectoryName(FullPath));
        }

        public bool Exists()
        {
            return Directory.Exists(FullPath);
        }

        public bool Move(out string error)
        {
            throw new NotImplementedException();
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
        public string Name => Directory.GetParent(FullPath).Name;
        public string FullPath 
        {
            get => sPath;
            set
            {
                sPath = value.Insert(value.Length - 1, "\\");
            }
        }
        private string sPath;
    }
}
