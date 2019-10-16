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
            if (Path.IsPathRooted(path))
                FullPath = path;
            else throw new ArgumentException($"'{path}' is not a rooted path");
        }
        public SimpleFile(FileSystemInfo file)
        {
            FullPath = file.FullName;
        }

        public static SimpleFile[] CreateArray(FileSystemInfo[] collection)
        {
            SimpleFile[] array = new SimpleFile[collection.Length];
            for (int i = 0; i < collection.Length; ++i)
                array[i] = new SimpleFile(collection[i]);
            return array;
        }
        public static SimpleFile[] CreateArray(string[] collection)
        {
            SimpleFile[] array = new SimpleFile[collection.Length];
            for (int i = 0; i < collection.Length; ++i)
                array[i] = new SimpleFile(collection[i]);
            return array;
        }

        public string Name 
        {
            get
            {
                if (IsFile) return Path.GetFileName(FullPath);
                else return Path.GetDirectoryName(FullPath);
            }
        }
        public string Parent => Directory.GetParent(FullPath).FullName;
        public bool Exists
        {
            get
            {
                if (IsFile) return File.Exists(FullPath);
                else return Directory.Exists(FullPath);
            }
        }
        public bool IsFile { get; private set; }
        public string FullPath
        {
            get => sPath;
            set
            {
                sPath = value;
                IsFile = Path.GetFileName(sPath) != string.Empty;
            }
        }
        public FileAction Action { get; set; }
        private string sPath;
    }
}