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
using BatchRename.Models;

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
        public Editor(object function) : this()
        {
            if (function is BatchFunction batch) 
            {
                FunctionType.SelectedItem = batch.Name;
            }
            else throw new ArgumentException("function is not BatchFunction");
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
            MessageBox.Show("Submitting!");
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
        public EditorViewModel ViewModel { get; private set; } = new EditorViewModel();
        private Cursor origin;
    }
}
