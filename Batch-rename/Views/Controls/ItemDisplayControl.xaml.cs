using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BatchRename.ViewModels;
using System.IO;
using BatchRename.Commands;
using BatchRename.Models;

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

        private void AddDraggedItem(object sender, DragEventArgs e)
        {
            string[] items = (string[])e.Data.GetData(DataFormats.FileDrop);
            ViewModel.AddDraggedItems(items);
        }
        private void AddDraggedActions(object sender, DragEventArgs e)
        {
            e.Data.GetData(DataFormats.Serializable);
        }

        private void OnObjectsDragged(object sender, DragEventArgs e)
        {
            foreach (string format in e.Data.GetFormats())
            {
                Debug.WriteLine($"{nameof(OnObjectsDragged)}: Convertible Formats:");
                Debug.WriteLine(format);
            }
        }
    }
}
