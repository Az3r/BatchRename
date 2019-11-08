using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatchRename.Views.Controls;
using BatchRename.Models;
using System.Windows.Controls;
namespace BatchRename.ViewModels
{
    public class EditorViewModel
    {
        public EditorViewModel() { }

        private readonly FunctionReplace replace = new FunctionReplace();
        private readonly FunctionMove move = new FunctionMove();
        private readonly FunctionGUID guid = new FunctionGUID();
        private readonly FunctionChangeFormat changeFormat = new FunctionChangeFormat();
        private readonly FunctionNormalize normalize = new FunctionNormalize();
        public Control CreateControl(string name)
        {
            if (name == replace.Name) return new ReplaceControl();
            if (name == move.Name) return new MoveControl();
            //if (name == guid.Name) return new FunctionGUID();
            if (name == changeFormat.Name) return new ChangeFormatControl();
            //if (name == normalize.Name) return new FunctionNormalize();
            return null;
        }

        public string[] FunctionNames { get; private set; } = new string[]
        {
            new FunctionReplace().Name,
            new FunctionMove().Name,
            new FunctionChangeFormat().Name,
            new FunctionNormalize().Name,
            new FunctionGUID().Name
        };
    }
}
