using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using System.Windows.Shapes;
using System.ComponentModel;
using BatchRename.ViewModels;
using BatchRename.Views.Controls;
using System.Runtime.CompilerServices;
namespace BatchRename.Views
{
    /// <summary>
    /// Interaction logic for Function_Creator.xaml
    /// </summary>
    public partial class ActionEditor : Window
    {
        public ActionEditor()
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
        }
        private void Submitted(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Submitting!");
            Close();
        }
        private void CanClose(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Canceled(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Canceling!");
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



        public EditorViewModel ViewModel { get; private set; } = new EditorViewModel();
        private Cursor origin;
    }
}
