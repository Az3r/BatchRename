using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using BatchRename.Models;
namespace BatchRename.Views.Controls
{
    /// <summary>
    /// Interaction logic for MoveControl.xaml
    /// </summary>
    public partial class MoveControl : UserControl
    {
        public MoveControl()
        {
            DataContext = ViewModel;
            InitializeComponent();
        }

        private void OnCheckInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !regex.IsMatch(e.Text);
        }
        private void OnCheckPastedData(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.StringFormat))
            {
                string text = e.DataObject.GetData(DataFormats.StringFormat).ToString();
                if (regex.IsMatch(text)) return;
            }
            e.CancelCommand();
        }
        private void OnShowResult(object sender, RoutedEventArgs e)
        {
            try
            {
                tbResult.Text = ViewModel.GetString(tbInput.Text);
                tbResult.IsEnabled = true;
            }
            catch (Exception)
            {
                tbResult.Text = "invalid input!";
                tbResult.IsEnabled = false;
            }
        }
        private static readonly Regex regex = new Regex("[0-9]+");
        public FunctionMove ViewModel { get; private set; } = new FunctionMove();
    }
}
