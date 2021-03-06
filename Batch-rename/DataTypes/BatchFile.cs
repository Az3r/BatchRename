﻿using BatchRename.Shared;
using System.IO;

namespace BatchRename.DataTypes
{
    /// <summary>
    /// Containing simple information of a file or folder such as name, full path, whether it exists or not
    /// </summary>
    public sealed class BatchFile : BatchPath
    {
        public BatchFile() { }
        public BatchFile(string path) { FullName = path; }
        public override bool Exists()
        {
            return File.Exists(FullName);
        }

        public override int Create(bool overwrite)
        {
            if (overwrite == false && Exists()) return Error.FILE_EXISTS;
            using (File.Create(FullName)) { }
            return Error.SUCCESS;
        }

        public override int Delete()
        {
            if (!Exists())
            {
                return Error.FILE_NOT_FOUND;
            }
            File.Delete(FullName);
            return Error.SUCCESS;
        }

        public override int DeleteAll()
        {
            return Delete();
        }

        public override int Move(string destination)
        {
            if (File.Exists(destination))
            {
                return Error.FILE_EXISTS;
            }
            File.Move(FullName, destination);
            return Error.SUCCESS;
        }
        public override BatchPath Clone()
        {
            return new BatchFile() { FullName = this.FullName };
        }
    }
}