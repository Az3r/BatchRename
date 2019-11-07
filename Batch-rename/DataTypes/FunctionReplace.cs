using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatchRename.Models;

namespace BatchRename.DataTypes
{
    public sealed class FunctionReplace : BatchFunction
    {
        public FunctionReplace()
        {
            Name = "Replace";
            args = new object[2] { "from", "to" };
            Handler = new StringDelegate((s, args) => s.Replace(args[0].ToString(), args[1].ToString()));
        }

        public string OldValue
        {
            get => args[0].ToString();
            set
            {
                args[0] = value;
                NotifyPropertyChanged();
            }
        }
        public string NewValue
        {
            get => args[1].ToString();
            set
            {
                args[1] = value;
                NotifyPropertyChanged();
            }
        }
    }
}
