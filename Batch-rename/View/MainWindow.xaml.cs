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
            InitializeComponent();
            DataContext = this;

            ActionItems = new ObservableCollection<ActionViewModel>()
            {
                new ActionViewModel(new ReplaceAction("i", "@"))
            };

            ReplaceAction action = new ReplaceAction("0b5", "@");

            string container = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Container");

            string[] files = Directory.GetFiles(container);
            List<BatchFile> collection = new List<BatchFile>();
            foreach (string file in files)
                collection.Add(new BatchFile(file));

            FileItems = new ObservableCollection<FileViewModel>(FileViewModel.CreateArray(collection.ToArray(), action));

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
            Parallel.ForEach(FileItems, (item) => item.Rename(true));
        }

        private void OnCreateAction(object sender, RoutedEventArgs e)
        {
            FunctionCreator actionEditor = new FunctionCreator();
            actionEditor.ShowDialog();
        }
    }
}
