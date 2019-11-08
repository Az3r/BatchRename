using System;
using System.Collections.Generic;
using System.Text;
using BatchRename.Models;
using StringLibrary;
namespace BatchRename.Models
{
    public sealed class FunctionChangeFormat : BatchFunction
    {
        public FunctionChangeFormat()
        {
            Name = "Change Format";
        }

        public override string GetString(string src)
        {
            return src.ChangeFormat(format);
        }

        public StringFormat Format
        {
            get => format;
            set
            {
                format = (StringFormat) Enum.Parse(typeof(StringFormat), value.ToString());
                NotifyPropertyChanged();
            }
        }

        public string[] Formats => Enum.GetNames(typeof(StringFormat));

        private StringFormat format;
    }
}
