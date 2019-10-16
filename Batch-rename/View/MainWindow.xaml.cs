using BatchRename.Model;
using BatchRename.ViewModel;
using System;
using System.Collections.ObjectModel;
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
            InitializeComponent();
            DataContext = this;

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog(null);
            ActionItems = new ObservableCollection<ActionViewModel>()
            {
                new ActionViewModel(new ReplaceAction("i", "@"))
            };

            ReplaceAction action = new ReplaceAction("0b5", "@");

            string container = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Container");
            FileViewModel[] collection = FileViewModel.CreateArray(BatchFile.CreateArray(Directory.GetFiles(container)), action);

            FileItems = new ObservableCollection<FileViewModel>(collection);

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

        private void OnExecute(object sender, RoutedEventArgs e)
        {
            //Parallel.ForEach(FileItems, (item) => item.Rename());
        }
    }
}
