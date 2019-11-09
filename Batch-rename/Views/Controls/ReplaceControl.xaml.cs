using BatchRename.Models;
using System;
using System.Windows;
using System.Windows.Controls;
namespace BatchRename.Views.Controls
{
    /// <summary>
    /// Interaction logic for ReplaceControl.xaml
    /// </summary>
    public partial class ReplaceControl : UserControl
    {
        public ReplaceControl()
        {
            DataContext = ViewModel;
            InitializeComponent();
        }
        public ReplaceControl(FunctionReplace vm)
        {
            ViewModel = vm;
            DataContext = vm;
            InitializeComponent();
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
        public FunctionReplace ViewModel { get; set; } = new FunctionReplace();
    }
}
