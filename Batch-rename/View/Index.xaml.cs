using BatchRename.Model;
using BatchRename.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
namespace BatchRename.View
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

        public ItemViewModel ViewModel { get; set; }

        private void OnCreateAction(object sender, RoutedEventArgs e)
        {

        }

        private void OnExpand(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            expanded = !expanded;
            if (expanded) 
            {
                ActionCol.Width = mSaveLength[0];
                SplitterCol.Width = mSaveLength[1];
            } 
            else
            {
                mSaveLength[0] = ActionCol.Width;
                mSaveLength[1] = SplitterCol.Width;
                SplitterCol.Width = ActionCol.Width = new GridLength(0, GridUnitType.Star);
            }
        }

        private bool expanded = true;
        private GridLength[] mSaveLength = new GridLength[2];
    }
}
