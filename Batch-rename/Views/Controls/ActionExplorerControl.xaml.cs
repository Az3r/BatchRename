using System.Windows.Controls;
using BatchRename.ViewModels;
using System.Collections.Generic;
using BatchRename.Models;
using System.Windows.Input;
using System.Windows;

namespace BatchRename.Views.Controls
{
    /// <summary>
    /// Interaction logic for ControlAction.xaml
    /// </summary>
    public partial class ActionExplorerControl : UserControl
    {
        public ActionExplorerControl()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        /*
         * Commands events
         */
        public void CanExpandControl(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void ExpandedControl(object sender, ExecutedRoutedEventArgs e)
        {
            Control param = e.Parameter as Control;
            if (param.Visibility == System.Windows.Visibility.Visible) param.Visibility = System.Windows.Visibility.Collapsed;
            else if (param.Visibility == System.Windows.Visibility.Collapsed) param.Visibility = System.Windows.Visibility.Visible;
        }
        public void CanCreate(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void Created(object sender, ExecutedRoutedEventArgs e)
        {
            /*
             * UPDATE THIS!!!
             */
            ViewModel.CreateFunction();
        }
        public void CanRefresh(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void Refreshed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.RefreshFavorite();
        }
        public void CanRemoveFavorite(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void RemovedFavorite(object sender, ExecutedRoutedEventArgs e)
        {
            List<BatchFunction> collection = FavoriteFunctions.SelectedItems as List<BatchFunction>;
            System.Diagnostics.Debug.WriteLine($"@{nameof(RemovedFavorite)}: Null collection: {collection is null}");
        }
        public void CanDelete(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void Deleted(object sender, ExecutedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"@{nameof(Deleted)}: deleting");
        }

        /*
         * Other events
         */
        private void OnButtonChangeFavorite(object sender, System.Windows.RoutedEventArgs e)
        {
            Button button = sender as Button;
            BatchFunction item = button.DataContext as BatchFunction;
            item.IsFavorite = !item.IsFavorite;
        }

        /*
         * Properties
         */
        public ActionExplorerViewModel ViewModel { get; private set; } = new ActionExplorerViewModel();
    }
}
