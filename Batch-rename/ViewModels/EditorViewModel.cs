using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatchRename.Views.Controls;
using BatchRename.Models;
namespace BatchRename.ViewModels
{
    public class EditorViewModel
    {
        public EditorViewModel() { }


        BatchFunction[] Functions { get; set; } = new BatchFunction[]
        {
            new FunctionReplace(),
            new FunctionMove(),
            new FunctionChangeFormat(),
        };

    }
}
