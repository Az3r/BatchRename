using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BatchRename.ViewModel;
namespace BatchRename.ViewModel.Command
{
    public class ItemAddCommand : Command
    {
        public override bool CanExecute(object parameter)
        {
            ItemViewModel viewModel = parameter as ItemViewModel;
            return true;
        }

        public override void Execute(object parameter)
        {
            ItemViewModel viewModel = parameter as ItemViewModel;
            viewModel.AddFileFromExplorer();
        }
    }
}
