using BatchRename.Models;
using BatchRename.ViewModels;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            // Assign commands

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
            Debug.WriteLine($"@{nameof(ExecutedSelectedFiles)}: Actions: {FilesDataGrid.SelectedItems.Count}");
        }
        public void CanExecuteAllFiles(object sender, CanExecuteRoutedEventArgs e)
        {
            if (FilesDataGrid?.Items.Count > 0)
                e.CanExecute = true;
            else e.CanExecute = false;
        }
        public void ExecutedAllFiles(object sender, ExecutedRoutedEventArgs e)
        {
            Debug.WriteLine($"@{nameof(ExecutedAllFiles)}: Actions: {FilesDataGrid.Items.Count}");
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
            else if(e.Data.GetData(typeof(BatchFunction[])) is BatchFunction[] functions)
            {
                foreach (BatchFunction fn in functions)
                {
                    ViewModel.SelectedFunctions.Add(fn);
                }
            }
        }
    }
}
