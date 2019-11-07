using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatchRename.Models;
namespace BatchRename.DataTypes
{
    public sealed class FunctionMove : BatchFunction
    {
        public FunctionMove() 
        {
            Name = "Move";
            args = new object[3];
            Handler = new StringDelegate((s, args) => Move(s, (int)args[0], (int)args[1], (int)args[2]));
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
        private string Move(string src, int index, int count, string dest, int position)
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
        private string Move(string str, int from, int to, int count)
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



    }
}
