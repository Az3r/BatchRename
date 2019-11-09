using BatchRename.Models;
using BatchRename.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BatchRename.Views
{
    /// <summary>
    /// Interaction logic for Function_Creator.xaml
    /// </summary>
    public partial class Editor : Window
    {
        public Editor()
        {
            Style = FindResource(typeof(Window)) as Style;
            DataContext = ViewModel;
            InitializeComponent();
        }
        /*
         * Command events
         */
        private void CanSubmit(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            if (FunctionDisplayer?.Child == null) e.CanExecute = false;
        }
        private void Submitted(object sender, ExecutedRoutedEventArgs e)
        {
            Control ctrl = FunctionDisplayer.Child as Control;
            ViewModel.ApplySetting(ctrl.DataContext);
            Close();
        }
        /*
         * Other events
         */
        private void OnDragging(object sender, MouseButtonEventArgs e)
        {
            origin = Cursor;
            Cursor = Cursors.Hand;
            DragMove();
        }

        private void OnDragged(object sender, MouseButtonEventArgs e)
        {
            Cursor = origin;
        }
        private void OnChangedControl(object sender, SelectionChangedEventArgs e)
        {
            if (FunctionDisplayer == null || e.AddedItems?.Count == 0) return;
            FunctionDisplayer.Child = ViewModel.CreateControl(e.AddedItems[0].ToString());
        }
        private void OnCancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /*
         * Public Methods
         */
        public void SetDisplayFunction(BatchFunction function)
        {
            FunctionDisplayer.Child = ViewModel.CreateControl(function);
        }

        public EditorViewModel ViewModel { get; private set; } = new EditorViewModel();
        private Cursor origin;
    }
}
