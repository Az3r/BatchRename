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
            args = new object[3];
            Handler = new StringDelegate((s, args) => s.Move(FirstFrom, FirstTo, Count));
        }

        public int FirstFrom
        {
            get => int.Parse(args[0].ToString());
            set
            {
                args[0] = value;
                NotifyPropertyChanged();
            }
        }
        public int FirstTo
        {
            get => int.Parse(args[1].ToString());
            set
            {
                args[0] = value;
                NotifyPropertyChanged();

            }
        }
        public int Count
        {
            get => int.Parse(args[2].ToString());
            set
            {
                args[0] = value;
                NotifyPropertyChanged();
            }
        }
    }
}
