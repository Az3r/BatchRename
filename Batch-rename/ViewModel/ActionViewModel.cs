using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatchRename.Model;
namespace BatchRename.ViewModel
{
    public class ActionViewModel
    {
        public ActionViewModel(FileAction action)
        {
             TxtName = action.Name;
        }

        public static ActionViewModel[] CreateArray(FileAction[] collection)
        {
            ActionViewModel[] array = new ActionViewModel[collection.Length];
            for (int i = 0; i < collection.Length; ++i)
                array[i] = new ActionViewModel(collection[i]);
            return array;
        }

        public string TxtName { get; private set; }

    }
}
