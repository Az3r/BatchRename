using System.Windows.Controls;
using BatchRename.ViewModels;
using System.Collections.Generic;
using System.Collections;
using BatchRename.Models;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;

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
            ViewModel.CreateFunction(Application.Current.MainWindow);
        }
        public void CanRefresh(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void Refreshed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.RefreshFavorite();
        }
        public void CanDelete(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            if (AllFunctions == null || FavoriteFunctions == null) e.CanExecute = false;
            else if (AllFunctions.SelectedItems.Count == 0 && FavoriteFunctions.SelectedItems.Count == 0) e.CanExecute = false;
            else
            {
                foreach (BatchFunction item in AllFunctions.SelectedItems)
                {
                    if (item.GetType() == typeof(FunctionGUID) || item.GetType() == typeof(FunctionNormalize))
                    {
                        e.CanExecute = false;
                        return;
                    }
                }
                foreach (BatchFunction item in FavoriteFunctions.SelectedItems)
                {
                    if (item.GetType() == typeof(FunctionGUID) || item.GetType() == typeof(FunctionNormalize))
                    {
                        e.CanExecute = false;
                        return;
                    }
                }
            }
        }
        public void Deleted(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.RemoveFunctions(FavoriteFunctions.SelectedItems);
            ViewModel.RemoveFunctions(AllFunctions.SelectedItems);
        }

        private void CanOpenProperty(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpenedProperty(object sender, ExecutedRoutedEventArgs e)
        {
            //if (param.Visibility == Visibility.Visible) param.Visibility = Visibility.Collapsed;
            UIElement param = e.Parameter as UIElement;
            // Collapse all element except one in e.param
            foreach (UIElement element in PropertiesPanel.Children)
            {
                element.Visibility = Visibility.Collapsed;
            }
            param.Visibility = Visibility.Visible;
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
        private void OnCreating(object sender, MouseButtonEventArgs e)
        {
            ListBox control = e.Source as ListBox;
            ViewModel.CreateFunction(Application.Current.MainWindow, control.SelectedItem as BatchFunction);
        }
        private void OnEditing(object sender, MouseButtonEventArgs e)
        {
            ListBox control = e.Source as ListBox;
            BatchFunction selected = control.SelectedItem as BatchFunction;
            ViewModel.EditFunction(Application.Current.MainWindow, selected);
        }

        private void OnBeginDragging(object sender, MouseButtonEventArgs e)
        {
            ListBox container = null;
            if (AllFunctions.SelectedItems.Count > 0) container = AllFunctions;
            if (FavoriteFunctions.SelectedItems.Count > 0) container = FavoriteFunctions;
            if (container == null) return;

            IList selecteds = container.SelectedItems;
            BatchFunction[] arr = new BatchFunction[selecteds.Count];
            selecteds.CopyTo(arr, 0);
            DataObject data = new DataObject(typeof(BatchFunction[]), arr);
            DragDrop.DoDragDrop(container, data, DragDropEffects.Link);
        }
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Unselect the other ListBox
            if (ReferenceEquals(e.Source, FavoriteFunctions))
            {
                AllFunctions.UnselectAll();
            }
            else FavoriteFunctions.UnselectAll();
        }
        /*
        * Properties
        */
        public ActionExplorerViewModel ViewModel { get; private set; } = new ActionExplorerViewModel();

    }
}