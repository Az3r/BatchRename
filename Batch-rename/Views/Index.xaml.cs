using BatchRename.ViewModels;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
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
            Style = FindResource(typeof(Window)) as Style;
            Application.Current.MainWindow = this;
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

        public ItemViewModel ViewModel { get; set; } = new ItemViewModel();
    }
}
