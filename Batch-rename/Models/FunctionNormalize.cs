using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringLibrary;
namespace BatchRename.Models
{
    public sealed class FunctionNormalize : BatchFunction
    {
        public FunctionNormalize()
        {
            Name = "Normalize";
            args = null;
            Handler = new StringDelegate((s, args) => s.Normalize());
        }
    }
}
