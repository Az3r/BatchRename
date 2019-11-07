using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename.Models
{
    public sealed class FunctionReplace : BatchFunction
    {
        public FunctionReplace()
        {
            Name = "Replace Function";
            args = new object[2] { "from", "to" };
            Handler = new StringDelegate((s, args) => Replace(s, args[0].ToString(), args[1].ToString()));
        }
        private string Replace(string str, string oldStr, string newStr)
        {
            return str.Replace(oldStr, newStr);
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
