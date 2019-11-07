using System;
using System.Text;

namespace StringLibrary
{
    /// <summary>
    /// Specify constants that define the way string is formatted
    /// </summary>
    public enum StringFormat
    {
        /// <summary>
        /// All characters are upper-case
        /// </summary>
        AllUpper,
        /// <summary>
        /// All characters are lower-case
        /// </summary>
        AllLower,
        /// <summary>
        /// No leading and trailing white-space characters, every word in string is seperated by one and only one space character
        /// </summary>
        Capitalize
    }
    /// <summary>
    /// an extension library for handling string
    /// </summary>
    public static class Library
    {
        /// <summary>
        /// From a given string, create a new string with specific format
        /// </summary>
        /// <param name="str">the string to be used for formatting</param>
        /// <param name="format">Type of format to be used</param>
        /// <returns>new string after formatting</returns>
        static public string ChangeFormat(string str, StringFormat format)
        {
            if (str == null) throw new ArgumentNullException($"{nameof(str)}");
            switch (format)
            {
                case StringFormat.AllUpper:
                    return str.ToUpper();
                case StringFormat.AllLower:
                    return str.ToLower();
                case StringFormat.Capitalize:
                    StringBuilder builder = new StringBuilder(str.Length);
                    string[] tokens = str.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string token in tokens)
                    {
                        StringBuilder temp = new StringBuilder(token);
                        temp[0] = char.ToUpper(temp[0]);
                        builder.Append(temp.ToString());
                    }
                    return builder.ToString();
                default:
                    throw new NotSupportedException($"Format {format} is not supported");
            }
        }

        /// <summary>
        /// Move <paramref name="count"/> characters of <paramref name="src"/> to position <paramref name="position"/> in <paramref name="dest"/>
        /// </summary>
        /// <param name="src">string whose characters will be moved</param>
        /// <param name="index">position of first character to be moved</param>
        /// <param name="dest">string where characters are moved to</param>
        /// <param name="position">position in destination string</param>
        /// <param name="count">number of characters to be moved</param>
        /// <returns>string containing <paramref name="dest"/> and substring of <paramref name="src"/></returns>
        static public string Move(string src, int index, int count, string dest, int position)
        {
            if (src == null || dest == null) throw new ArgumentNullException($"{nameof(src)} or {nameof(dest)} is null");
            if (position < 0 || count < 0) throw new ArgumentOutOfRangeException($"{nameof(position)} < 0 or {nameof(count)} < 0");

            string result = dest.Insert(position, src.Substring(index, count));

            return result;
        }
        /// <summary>
        /// Move <paramref name="count"/> characters within <paramref name="str"/> from position <paramref name="from"/> to position <paramref name="to"/>
        /// <para>Function returns <paramref name="str"/> if <paramref name="from"/> equals to <paramref name="to"/></para>
        /// </summary>
        /// <param name="str">string contaning characters to be moved</param>
        /// <param name="from">position of first character to be moved</param>
        /// <param name="to">position which first character moves to</param>
        /// <param name="count">number of moved characters</param>
        /// <returns>string after changing characters' position</returns>
        static public string Move(string str, int from, int to, int count)
        {
            if (str == null) throw new ArgumentNullException($"{nameof(str)} is null");
            if (from < 0) throw new ArgumentOutOfRangeException($"{nameof(from)}");
            if (to < 0) throw new ArgumentOutOfRangeException($"{nameof(to)}");
            if (count < 0) throw new ArgumentOutOfRangeException($"{nameof(count)}");
            if (from > to) throw new ArgumentException($"{nameof(from)} > {nameof(to)}");
            if (from == to) return str;
            StringBuilder result = new StringBuilder(str);
            for (int i = 0; i < count; ++i)
            {
                char ch = result[i + from];
                result[i + from] = result[i + to];
                result[i + to] = ch;
            }
            return result.ToString();
        }

        /// <summary>
        /// <seealso cref="ChangeFormat(string, StringFormat)"/> with format is <seealso cref="StringFormat.Capitalize"/>
        /// </summary>
        /// <param name="str">the string to be normalized</param>
        /// <returns>new string after normalizing</returns>
        static public string Normalize(string str)
        {
            return ChangeFormat(str, StringFormat.Capitalize);
        }

        /// <summary>
        /// get a new <seealso cref="Guid"/> value
        /// </summary>
        /// <returns>new <see cref="Guid"/> value</returns>
        static public string GetGUID()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
