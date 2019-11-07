using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BatchRename.Shared;

namespace BatchRename.DataTypes
{
    public sealed class BatchDirectory : BatchPath
    {
        public BatchDirectory() { }
        public BatchDirectory(string path) { FullName = path; }

        public override int Create(bool overwrite)
        {
            bool bExists = Exists();
            if (bExists && !overwrite)
            {
                return Error.FOLDER_EXISTS;
            }
            else if (bExists && overwrite)
            {
                Directory.Delete(FullName, true);
            }
            Directory.CreateDirectory(FullName);
            return Error.SUCCESS;
        }

        public override int Delete()
        {
            if (!Exists())
            {
                return Error.FOLDER_NOT_FOUND;
            }
            else if (!IsEmpty())
            {
                return Error.FOLDER_NOT_EMPTY;
            }
            Directory.Delete(FullName);
            return Error.SUCCESS;
        }

        public override int DeleteAll()
        {
            if (!Exists())
            {
                return Error.FOLDER_NOT_FOUND;
            }
            Directory.Delete(FullName, true);
            return Error.SUCCESS;
        }

        public override int Move(string destination)
        {
            if (Directory.Exists(destination))
            {
                return Error.FOLDER_NOT_FOUND;
            }
            else if(!Exists())
            {
                return Error.FOLDER_NOT_FOUND;
            }
            Directory.Move(FullName, destination);
            return Error.SUCCESS;
        }
        public bool IsEmpty()
        {
            return !Directory.EnumerateFileSystemEntries(FullName).Any();
        }
        public override bool Exists()
        {
            return Directory.Exists(FullName);
        }
        public override BatchPath Clone()
        {
            return new BatchDirectory() { FullName = this.FullName };
        }
    }
}
