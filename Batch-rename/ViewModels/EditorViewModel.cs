using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatchRename.Views.Controls;
using BatchRename.Models;
using System.Windows.Controls;
using BatchRename.Shared;
namespace BatchRename.ViewModels
{
    public class EditorViewModel : EventNotifier
    {
        public EditorViewModel() { }

        private FunctionReplace replace = new FunctionReplace();
        private FunctionMove move = new FunctionMove();
        private FunctionChangeFormat changeFormat = new FunctionChangeFormat();

        private void reset()
        {
            replace = new FunctionReplace();
            move = new FunctionMove();
            changeFormat = new FunctionChangeFormat();
        }
        public Control CreateControl(string name)
        {
            if (name == replace.Name) return new ReplaceControl();
            if (name == move.Name) return new MoveControl();
            if (name == changeFormat.Name) return new ChangeFormatControl();
            return null;
        }

        public string[] FunctionNames { get; private set; } = new string[]
        {
            new FunctionReplace().Name,
            new FunctionMove().Name,
            new FunctionChangeFormat().Name,
        };

        public void ApplySetting(object function)
        {
            GeneratedFunction = null;
            if (function is FunctionReplace)
            {
                GeneratedFunction = new FunctionReplace()
                {
                    IsFavorite = this.IsFavorite,
                    Name = this.Name,
                    NewValue = (function as FunctionReplace).NewValue,
                    OldValue = (function as FunctionReplace).OldValue
                };
            }
            else if (function is FunctionMove fnMove)
            {

                GeneratedFunction = new FunctionMove()
                {
                    IsFavorite = this.IsFavorite,
                    FirstFrom = fnMove.FirstFrom,
                    FirstTo = fnMove.FirstTo,
                    Count = fnMove.Count
                };
            }
            else if (function is FunctionChangeFormat fnChangeFormat)
            {
                GeneratedFunction = new FunctionChangeFormat()
                {
                    IsFavorite = this.IsFavorite,
                    Name = this.Name,
                    Format = fnChangeFormat.Format
                };
            }
        }

        public string Name
        {
            get => mName;
            set
            {
                mName = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsFavorite
        {
            get => mFavorite;
            set
            {
                mFavorite = value;
                NotifyPropertyChanged();
            }
        }
        public BatchFunction GeneratedFunction { get; private set; }

        private string mName = "My Action";
        private bool mFavorite = false;
    }
}
