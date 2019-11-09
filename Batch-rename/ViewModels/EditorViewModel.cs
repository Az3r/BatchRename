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

        public Control CreateControl(string name)
        {
            if (name == replace.Name) return new ReplaceControl();
            if (name == move.Name) return new MoveControl();
            if (name == changeFormat.Name) return new ChangeFormatControl();
            return null;
        }

        public Control CreateControl(BatchFunction function)
        {
            if (function is FunctionReplace) return new ReplaceControl() { DataContext = function.Clone() };
            else if (function is FunctionMove) return new MoveControl() { DataContext = function.Clone() };
            else if (function is FunctionChangeFormat) return new ChangeFormatControl() { DataContext = function.Clone() };
            return null;
        }

        public string[] FunctionNames { get; private set; } = new string[]
        {
            new FunctionReplace().Name,
            new FunctionMove().Name,
            new FunctionChangeFormat().Name,
        };

        public void ApplySetting(object control)
        {
            GeneratedFunction = control as BatchFunction;
        }
        public BatchFunction GeneratedFunction { get; private set; }
    }
}
