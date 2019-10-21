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
using BatchRename.ViewModel;
using System.IO;
namespace BatchRename.View.Control
{
    /// <summary>
    /// Interaction logic for ControlPath.xaml
    /// </summary>
    public partial class ItemDisplayControl : UserControl
    {
        public ItemDisplayControl()
        {
            DataContext = ViewModel;
            InitializeComponent();
        }

        public ItemViewModel ViewModel { get; set; } = new ItemViewModel();

        private void OnShowDescription(object sender, RoutedEventArgs e)
        {
            colDescription.Visibility = Visibility.Visible;
        }

        private void OnHideDescription(object sender, RoutedEventArgs e)
        {
            colDescription.Visibility = Visibility.Collapsed;
        }

        private void OnAddFiles(object sender, RoutedEventArgs e)
        {
            ViewModel.AddFileFromExplorder();
        }

        private void AddDraggedItem(object sender, DragEventArgs e)
        {
            string[] items = (string[])e.Data.GetData(DataFormats.FileDrop);
            ViewModel.AddDraggedItems(items);
        }
    }
}
