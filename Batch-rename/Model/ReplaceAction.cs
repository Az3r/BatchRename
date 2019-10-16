using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace BatchRename
{
    public sealed class ReplaceAction : FileAction
    {
        public ReplaceAction(string oldValue, string newValue)
        {
            Name = "Replace";
            OldStr = oldValue;
            NewStr = newValue;

            ActionHandler = new ActionDelegate(Replace);
        }
        private void Replace(SimpleFile target)
        {
            string newName = target.Name.Replace(OldStr, NewStr);
            target.FullPath = Path.Combine(target.Parent, newName);
        }

        public string OldStr { get; set; }
        public string NewStr { get; set; }
    }
}
