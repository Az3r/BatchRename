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
            Name = "Change Case";
            args = new object[1] { StringFormat.None };
            Handler = new StringDelegate((s, args) => s.ChangeFormat(Format));
        }

        public StringFormat Format
        {
            get => (StringFormat)Enum.Parse(typeof(StringFormat), args[0].ToString());
            set
            {
                args[0] = value;
                NotifyPropertyChanged();
            }
        }
    }
}
