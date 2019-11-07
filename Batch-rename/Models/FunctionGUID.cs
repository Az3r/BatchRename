using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringLibrary;
namespace BatchRename.Models
{
    public sealed class FunctionGUID : BatchFunction
    {
        public FunctionGUID()
        {
            Name = "GUID";
            args = null;
            Handler = new StringDelegate((s, args) => StringExtension.GetGUID());
        }
    }
}
