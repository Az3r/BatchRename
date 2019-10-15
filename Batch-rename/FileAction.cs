using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class FileAction
    {
        public FileAction() 
        {
            ActionHandler = new ActionDelegate((target) => target);
        }
        public async Task<SimpleFile> Execute(SimpleFile target)
        {
            Task task = new Task(() =>
            {
                ActionHandler?.Invoke(target);
            });
            await task;
            return target;
        }
        public delegate SimpleFile ActionDelegate(SimpleFile target);
        public ActionDelegate ActionHandler { get; set; }
        public string Name { get; set; }

    }
}
