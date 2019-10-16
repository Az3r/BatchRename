using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Microsoft.Win32;
namespace BatchRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            string container = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Container");
            FileViewModel[] collection = FileViewModel.CreateArray(SimpleFile.CreateArray(Directory.GetFiles(container)));

            FileItems = new ObservableCollection<FileViewModel>(collection);

            ActionItems = new ObservableCollection<ActionViewModel>()
            {
                new ActionViewModel(new ReplaceAction("i", "@"))
            };

            /* Create test files
            DirectoryInfo dirInfo = Directory.CreateDirectory(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Container"));
            CreateFile(dirInfo.FullName, 100);
            */
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

        public ObservableCollection<FileViewModel> FileItems { get; private set; }
        public ObservableCollection<ActionViewModel> ActionItems { get; private set; }
    }
}
