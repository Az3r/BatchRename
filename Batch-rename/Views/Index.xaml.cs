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
            CreateFile(Path.Combine(System.Environment.CurrentDirectory, "Container"), 1000);
        }

        private void CreateFile(string directory, int n)
        {
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            try
            {
                Parallel.For(0, n, (i) =>
                {
                    using (File.Create($"{directory}/{System.IO.Path.GetRandomFileName()}")) { }
                });
            }
            catch (System.Exception e)
            {
                MessageBox.Show($"Unable to create test files\nError Message: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public ItemViewModel ViewModel { get; set; } = new ItemViewModel();

        private void OnOpenExplorer(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.O && System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftCtrl))
            {
                System.Diagnostics.Process.Start("explorer.exe",System.Environment.CurrentDirectory);
            }
        }
    }
}
