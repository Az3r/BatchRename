using BatchRename.Models;
using BatchRename.ViewModels;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BatchRename.Views.Controls
{
    /// <summary>
    /// Interaction logic for ControlPath.xaml
    /// </summary>
    public partial class ItemDisplayControl : UserControl
    {
        public ItemViewModel ViewModel { get; set; } = new ItemViewModel();
        public ItemDisplayControl()
        {
            // Assign datacontext
            DataContext = ViewModel;
            // Initialize
            InitializeComponent();
        }

        /*
         * Commands Events
         */

        public void CanAddFiles(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void AddedFiles(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.AddFileFromExplorer();
        }
        public void CanDeleteFiles(object sender, CanExecuteRoutedEventArgs e)
        {
            if (FilesDataGrid?.SelectedItems.Count > 0)
                e.CanExecute = true;
            else e.CanExecute = false;
        }
        public void DeletedFiles(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.RemoveItems(FilesDataGrid.SelectedItems);
        }
        public void CanExecuteSelectedFiles(object sender, CanExecuteRoutedEventArgs e)
        {
            if (FilesDataGrid?.SelectedItems.Count > 0)
                e.CanExecute = true;
            else e.CanExecute = false;
        }
        public void ExecutedSelectedFiles(object sender, ExecutedRoutedEventArgs e)
        {
            ShowDescription.IsChecked = true;
            this.Cursor = Cursors.Wait;
            ViewModel.RenameSelected(FilesDataGrid.SelectedItems);
            this.Cursor = Cursors.Arrow;
            FilesDataGrid.UnselectAll();
        }
        public void CanExecuteAllFiles(object sender, CanExecuteRoutedEventArgs e)
        {
            if (FilesDataGrid?.Items.Count > 0)
                e.CanExecute = true;
            else e.CanExecute = false;
        }
        public void ExecutedAllFiles(object sender, ExecutedRoutedEventArgs e)
        {
            ShowDescription.IsChecked = true;
            this.Cursor = Cursors.Wait;
            ViewModel.RenameAll();
            this.Cursor = Cursors.Arrow;
            FilesDataGrid.UnselectAll();
        }
        private void CanRemoveFunction(object sender, CanExecuteRoutedEventArgs e)
        {
            ListBox param = e.Parameter as ListBox;
            e.CanExecute = true;
            if (param?.SelectedItems.Count == 0) e.CanExecute = false;
        }

        private void RemovedFunction(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.RemoveFunctions(SelectedPanel.SelectedItems);
        }
        public void CanExpandControl(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void ExpandedControl(object sender, ExecutedRoutedEventArgs e)
        {
            Control param = e.Parameter as Control;
            if (param.Opacity == 0) param.Opacity = 1;
            else if (param.Opacity == 1) param.Opacity = 0;
            //if (param.Visibility == System.Windows.Visibility.Visible) param.Visibility = System.Windows.Visibility.Collapsed;
            //else if (param.Visibility == System.Windows.Visibility.Collapsed) param.Visibility = System.Windows.Visibility.Visible;
        }
        /*
         * other events
         */
        private void OnShowDescription(object sender, RoutedEventArgs e)
        {
            colDescription.Visibility = Visibility.Visible;
        }
        private void OnHideDescription(object sender, RoutedEventArgs e)
        {
            colDescription.Visibility = Visibility.Collapsed;
        }
        private void OnDropping(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is string[] files)
            {
                ViewModel.AddFiles(files);
            }
            else if (e.Data.GetData(typeof(BatchFunction[])) is BatchFunction[] functions)
            {
                ViewModel.AddFunctions(functions);
            }
        }
        private void OnChangeDisplay(object sender, RoutedEventArgs e)
        {
            ViewModel.RefreshDisplay();
        }

        private void OnFunctionSelected(object sender, SelectionChangedEventArgs e)
        {
            ListBox control = e.Source as ListBox;
            if (control?.SelectedItems.Count > 0) GuideLabel.Content = "Press 'Del' To Remove Selected Function";
            else GuideLabel.Content = "Drag Function To Here";
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point pos = e.GetPosition(sender as UIElement);
            HitTestResult result = VisualTreeHelper.HitTest(SelectedPanel, pos);
            if (result == null) SelectedPanel.UnselectAll();
        }
    }
}
