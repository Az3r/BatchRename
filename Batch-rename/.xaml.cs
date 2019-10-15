using System;
using System.Collections.Generic;
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
using System.IO;
namespace Batch_rename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Items = new List<FileViewModel>();
            string container = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Container");
            foreach (string name in Directory.GetFiles(container))
            {
                Items.Add(new FileViewModel(new FileInfo(name)));
            }
            DataContext = this;
            //DirectoryInfo dirInfo = Directory.CreateDirectory(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Container"));
            //CreateFile(dirInfo.FullName, 100);
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

        public List<FileViewModel> Items { get; private set; }
        //private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    if (e.PreviousSize.Width == e.NewSize.Width) return;
        //    Grid grid = sender as Grid;
        //    GridView gv = FileView.View as GridView;
        //    for (int i = 0; i < grid.ColumnDefinitions.Count; ++i)
        //    {
        //        // Change columns' size so that the ratio to grid does not change
        //        if (double.IsNaN(gv.Columns[i].Width))
        //            gv.Columns[i].Width = grid.ColumnDefinitions[i].ActualWidth;
        //        else
        //            gv.Columns[i].Width = gv.Columns[i].Width / e.PreviousSize.Width * e.NewSize.Width;
        //    }
        //}


    }
}
