using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using BatchRename.Shared;

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
            if (overwrite) using (File.Create(FullName)) { }
            else if (Exists())
            {
                return Error.FILE_EXISTS;
            }
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