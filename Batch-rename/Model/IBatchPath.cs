using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename.Model
{
    interface IBatchPath
    {
        string GetParent();
        bool Exists();
        bool Create(bool overwrite, out string error);
        bool Delete(out string error);
        bool DeleteAll(out string error);
        bool Move(string destination, out string error);
    }
}
