using BatchRename.Models;
using BatchRename.Shared;
using BatchRename.Views.Controls;
using System.Windows.Controls;
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
            if (function is FunctionReplace replace) return new ReplaceControl(replace.Clone() as FunctionReplace);
            else if (function is FunctionMove move) return new MoveControl(move.Clone() as FunctionMove);
            else if (function is FunctionChangeFormat format) return new ChangeFormatControl(format.Clone() as FunctionChangeFormat);
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
