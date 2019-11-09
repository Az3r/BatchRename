using BatchRename.Models;
using System;
using System.Windows;
using System.Windows.Controls;
namespace BatchRename.Views.Controls
{
    /// <summary>
    /// Interaction logic for NewCaseControl.xaml
    /// </summary>
    public partial class ChangeFormatControl : UserControl
    {
        public ChangeFormatControl()
        {
            DataContext = ViewModel;
            InitializeComponent();
        }
        public ChangeFormatControl(FunctionChangeFormat vm)
        {
            ViewModel = vm;
            DataContext = ViewModel;
            InitializeComponent();
        }
        public FunctionChangeFormat ViewModel { get; set; } = new FunctionChangeFormat();

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
    }
}
