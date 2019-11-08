using BatchRename.Models;
using BatchRename.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
namespace BatchRename.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = ViewModel;
            InitializeComponent();
            Style = FindResource(typeof(Window)) as Style;
        }

        /*
         * Commands events
         */
        private void CanExpand(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Expanded(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            // Collapse all element except one in e.param
            foreach (UIElement element in TabPanel.Children)
            {
                element.Visibility = Visibility.Collapsed;
            }
            UIElement param = e.Parameter as UIElement;
            param.Visibility = Visibility.Visible;
        }

        private void CreateFile(string directory, int n)
        {
            if (Directory.Exists(directory))
            {
                Parallel.For(0, n, (i) =>
                {
                    using (File.Create($"{directory}/{System.IO.Path.GetRandomFileName()}")) { }
                });
            }
            else throw new DirectoryNotFoundException($"{directory}");
        }
        private void OnOpenEditor(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListBox control = e.Source as ListBox;
            Editor window = new Editor(control.SelectedItem) { Owner = this };
            window.ShowDialog();
            EditorViewModel context = window.DataContext as EditorViewModel;
            if(context.GeneratedFunction != null)
            {
                ViewModel.Actions.Add(context.GeneratedFunction);
            }
        }
        public ItemViewModel ViewModel { get; set; } = new ItemViewModel();
    }
}
