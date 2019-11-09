using BatchRename.Models;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        public MoveControl(FunctionMove move)
        {
            ViewModel = move;
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
        public FunctionMove ViewModel { get; set; } = new FunctionMove();
    }
}
