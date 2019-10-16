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
    public class BatchFile : IBatchPath
    {
        private BatchFile() { FullPath = string.Empty; }
        public BatchFile(string path) { FullPath = path; }

        public string GetParent()
        {
            return Path.GetDirectoryName(FullPath);
        }

        public bool Exists()
        {
            return File.Exists(FullPath);
        }

        public bool Create(bool overwrite, out string error)
        {
            error = string.Empty;
            if (overwrite) using (File.Create(FullPath)) { }
            else if (Exists())
            {
                error = GetMessage(ErrorType.AlreadyExisted);
                return false;
            }
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
            File.Delete(FullPath);
            return true;
        }

        public bool DeleteAll(out string error)
        {
            return Delete(out error);
        }

        public bool Move(string destination, out string error)
        {
            error = string.Empty;
            if (File.Exists(destination))
            {
                error = GetMessage(ErrorType.AlreadyExisted);
                return false;
            }
            File.Move(FullPath, destination);
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
        private enum ErrorType
        {
            AlreadyExisted = 0,
            NotExists = 1
        }

        public string Name => Path.GetFileName(FullPath);
        public string FullPath { get; set; }
    }
}