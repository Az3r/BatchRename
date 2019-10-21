using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename.ViewModel.Command
{
    public class ItemRemoveCommand : Command
    {
        public override bool CanExecute(object parameter)
        {
            ItemViewModel vm = parameter as ItemViewModel;
            return vm?.SelectedItems?.Count > 0;
        }

        public override void Execute(object parameter)
        {
            ItemViewModel vm = parameter as ItemViewModel;
            vm.RemoveSelectedItems();
        }
    }
}
