using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatchRename.ViewModel;
namespace BatchRename.ViewModel.Command
{
    public class ItemExecuteSelectedCommand : Command
    {
        public override bool CanExecute(object parameter)
        {
            ItemViewModel vm = parameter as ItemViewModel;
            return (vm?.SelectedActions?.Count > 0) && vm?.SelectedItems?.Count > 0;
        }

        public override void Execute(object parameter)
        {
            ItemViewModel vm = parameter as ItemViewModel;
            Debug.WriteLine($"Execute {vm?.SelectedItems?.Count} selected files and folders!");
        }
    }
}
