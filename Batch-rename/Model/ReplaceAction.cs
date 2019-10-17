using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace BatchRename.Model
{
    public sealed class ReplaceAction : BaseAction
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
        private void Replace(PathInfo target)
        {
            string newName = target.GetName().Replace(OldStr, NewStr);
            target.FullName = Path.Combine(target.GetParent(), newName);
        }

        public string OldStr { get; set; }
        public string NewStr { get; set; }
    }
}
