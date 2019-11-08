using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatchRename.Models;
using StringLibrary;
namespace BatchRename.Models
{
    public sealed class FunctionMove : BatchFunction
    {
        public FunctionMove()
        {
            Name = "Move";
        }

        public override string GetString(string src)
        {
            return src.Move(FirstFrom, FirstTo, Count);
        }
        public int FirstFrom
        {
            get => args[0];
            set
            {
                args[0] = value;
                NotifyPropertyChanged();
            }
        }
        public int FirstTo
        {
            get => args[1];
            set
            {
                args[1] = value;
                NotifyPropertyChanged();

            }
        }
        public int Count
        {
            get => args[2];
            set
            {
                args[2] = value;
                NotifyPropertyChanged();
            }
        }

        private int[] args = new int[3];
    }
}
