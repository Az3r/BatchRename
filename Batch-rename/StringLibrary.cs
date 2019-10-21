using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class StringLibrary
    {
        public static string Capitalize(string target, StringCase stringCase)
        {
            StringBuilder builder;
            switch (stringCase)
            {
                case StringCase.AllUpper:
                    builder = new StringBuilder(target.ToUpper());
                    break;
                case StringCase.AllLower:
                    builder = new StringBuilder(target.ToLower());
                    break;
                case StringCase.Capitalize:
                    builder = new StringBuilder(target.ToLower());
                    builder[0] = char.ToUpper(builder[0]);
                    for (int i = 1; i < target.Length; ++i)
                    {
                        if (builder[i] == ' ' && i + 1 < target.Length)
                            builder[i + 1] = char.ToUpper(builder[i + 1]);
                    }
                    break;
                default:
                    throw new NotSupportedException($"{stringCase} is not supported");
            }
            return builder.ToString();
        }
        public static string NormalizeName(string target)
        {
            target = target.Trim();
            StringBuilder builder = new StringBuilder(target.Length);
            builder.Append(char.ToUpper(target[0]));
            for (int i = 1; i < target.Length; ++i)
            {
                if (target[i] != ' ' && target[i - 1] == ' ') builder.Append(char.ToUpper(target[i]));
                else if (target[i] != ' ') builder.Append(char.ToLower(target[i]));
                else if (target[i - 1] == ' ') continue;
            }
            return builder.ToString();
        }
        public string GetGuid()
        {
            return Guid.NewGuid().ToString();
        }
        public static string Move(string target, bool isbnFirst)
        {
            string section1 = target.Substring(0, 13);
            string bar = target.Substring(13, 3);
            string section2 = target.Substring(16);
            if (IsISBN(section1))
            {
                if (isbnFirst) return target;
                return section2 + bar + section1;
            }
            else
            {
                if (isbnFirst) return section2 + bar + section1;
                return target;
            }
        }
        private static bool IsISBN(string str)
        {
            if (str.Length != 13) return false;
            int count = 0;
            int start = str.IndexOf('-');
            while (start >= 0)
            {
                ++count;
                start = str.IndexOf('-', start + 1);
            }
            return count == 3;
        }
        public enum StringCase {AllUpper = 0, AllLower = 1, Capitalize = 2 };
    }
}
