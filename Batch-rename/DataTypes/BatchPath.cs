using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BatchRename.Shared;
using StringLibrary;
namespace BatchRename.DataTypes
{
    /// <summary>
    /// <see langword="abstract"/> class for <see cref="BatchFile"/> and <see cref="BatchDirectory"/>
    /// </summary>
    public abstract class BatchPath : EventNotifier, IEquatable<BatchPath>
    {
        public abstract int Create(bool overwrite);
        public abstract int Delete();
        public abstract int DeleteAll();
        public abstract int Move(string destination);
        public abstract bool Exists();
        public abstract BatchPath Clone();
        static public bool IsFile(string path) => File.Exists(path);
        static public bool IsDirectory(string path) => Directory.Exists(path);
        public bool IsFile() => File.Exists(FullName);
        public bool IsDirectory() => Directory.Exists(FullName);
        public virtual string GetParent()
        {
            return Path.GetDirectoryName(FullName);
        }
        public bool Equals(BatchPath other)
        {
            return other != null && other.FullName.Equals(FullName, StringComparison.Ordinal);
        }
        protected static bool IsPathAbsolute(string path)
        {
            return Path.IsPathRooted(path) && !Path.GetPathRoot(path).Equals(Path.DirectorySeparatorChar.ToString(), StringComparison.OrdinalIgnoreCase);
        }

        public virtual string Name => Path.GetFileName(FullName);
        public virtual string FullName 
        {
            get => mFullName;
            set
            {
                mFullName = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Name));
            }
        }

        private string mFullName = nameof(BatchPath);
    }
}
