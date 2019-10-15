using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace BatchRename
{
    /// <summary>
    /// Containing simple information of a file or folder such as name, full path, whether it exists or not
    /// </summary>
    public class SimpleFile
    {
        public SimpleFile(string path) 
        {
            string name = Path.GetFileName(path);
            if (name == string.Empty)
            {
                name = Path.GetDirectoryName(path);
            }

            sName = name;
            sPath = path;
            bExists = Directory.Exists(path) || File.Exists(path);
        }
        public SimpleFile(DirectoryInfo directory)
        {
            sName = directory.Name;
            sPath = directory.FullName;
            bExists = directory.Exists;
        }
        public SimpleFile(FileInfo file) 
        {
            sName = file.Name;
            sPath = file.FullName;
            bExists = file.Exists;
        }
        public string Name => sName;
        public string FullPath => sPath;
        public bool Exists => bExists;


        private readonly string sName;
        private readonly string sPath;
        private readonly bool bExists;
    }
}