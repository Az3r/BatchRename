using System;
using System.Collections.Generic;
using System.Text;
namespace BatchRename.Models
{
    public sealed class FunctionChangeCase : BatchFunction
    {
        public enum StringCase
        {
            AllUpper,
            AllLower,
            Capitalize
        }
        public FunctionChangeCase()
        {
            Name = "Change Case Function";
            args = new object[1] { StringCase.Capitalize };
            Handler = new StringDelegate((s, args) => ChangeCase(s, (StringCase)args[0]));
        }
        private static string ChangeCase(string str, StringCase strCase)
        {
            StringBuilder builder;
            switch (strCase)
            {
                case StringCase.AllUpper:
                    builder = new StringBuilder(str.ToUpper());
                    break;
                case StringCase.AllLower:
                    builder = new StringBuilder(str.ToLower());
                    break;
                case StringCase.Capitalize:
                    builder = new StringBuilder(str.Length);
                    builder.Append(char.ToUpper(str[0]));
                    for (int i = 1; i < str.Length; ++i)
                    {
                        if (str[i] == ' ' && i + 1 < str.Length)
                            builder.Append(char.ToUpper(builder[i + 1]));
                        else
                            builder.Append(char.ToLower(builder[i + 1]));
                    }
                    break;
                default:
                    throw new NotSupportedException($"CaseType {strCase} is not supported");
            }
            return builder.ToString();
        }
        static public StringCase[] GetStringCases()
        {
            return (StringCase[])Enum.GetValues(typeof(StringCase));
        }

        public StringCase Case
        {
            get => (StringCase)args[0];
            set
            {
                args[0] = value;
                NotifyPropertyChanged();
            }
        }
    }
}
