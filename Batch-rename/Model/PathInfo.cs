using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename.Model
{
    /// <summary>
    /// <see langword="abstract"/> class for <see cref="BatchFile"/> and <see cref="BatchDirectory"/>
    /// </summary>
    public abstract class PathInfo
    {
        public abstract bool Create(bool overwrite, out string error);
        public abstract bool Delete(out string error);
        public abstract bool DeleteAll(out string error);
        public abstract bool Move(string destination, out string error);
        public abstract bool Exists();
        public abstract string GetParent();
        public abstract string GetName();
        public abstract PathInfo Clone();
        public abstract string FullName { get; set; }

    }
}
