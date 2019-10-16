using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace BatchRename.Model
{
    public sealed class ReplaceAction : FileAction
    {
        public ReplaceAction()
        {
            Name = "Replace";
            ActionHandler = new ActionDelegate(Replace);
        }
        public ReplaceAction(string oldValue, string newValue) : this()
        {
            OldStr = oldValue;
            NewStr = newValue;
        }
        private void Replace(BatchFile target)
        {
            string newName = target.Name.Replace(OldStr, NewStr);
            target.FullPath = Path.Combine(target.Parent, newName);
        }

        public string OldStr { get; set; }
        public string NewStr { get; set; }
    }
}
