using BatchRename.Models;
using BatchRename.Shared;
using BatchRename.Views;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
namespace BatchRename.ViewModels
{
    public class ActionExplorerViewModel : EventNotifier
    {
        public ActionExplorerViewModel()
        {
            Functions.Add(new FunctionNormalize());
            Functions.Add(new FunctionGUID());

            RefreshFavorite();
        }

        public void RefreshFavorite()
        {
            NotifyPropertyChanged(nameof(FavoriteFunctions));
        }
        public void CreateFunction(Window owner)
        {
            Editor window = new Editor() { Owner = owner };
            window.ShowDialog();
            EditorViewModel context = window.DataContext as EditorViewModel;
            if (context.GeneratedFunction != null) Functions.Add(context.GeneratedFunction);
        }
        public void CreateFunction(Window owner, BatchFunction function)
        {
            Editor window = new Editor() { Owner = owner };
            window.SetDisplayFunction(function);
            window.ShowDialog();
            EditorViewModel context = window.DataContext as EditorViewModel;
            if (context.GeneratedFunction != null) Functions.Add(context.GeneratedFunction);
        }
        public void EditFunction(Window owner, BatchFunction function)
        {
            Editor window = new Editor() { Owner = owner };
            window.SetDisplayFunction(function);
            window.ShowDialog();
            EditorViewModel context = window.DataContext as EditorViewModel;
            if (context.GeneratedFunction != null)
            {
                if (function.GetType() != context.GeneratedFunction.GetType())
                {
                    Functions.Add(context.GeneratedFunction);
                }
                else
                {
                    int i = Functions.IndexOf(function);
                    Functions.RemoveAt(i);
                    if (i >= Functions.Count) Functions.Add(context.GeneratedFunction);
                    else Functions.Insert(i, context.GeneratedFunction);
                }

            }
        }
        public void RemoveFunctions(IList collection)
        {
            SortedSet<int> indexes = new SortedSet<int>();
            for (int i = 0; i < collection.Count; i++)
            {
                if (Functions.IndexOf(collection[i] as BatchFunction) > 0) indexes.Add(i);
            }
            for (int i = indexes.Count - 1; i >= 0; i--)
            {
                Functions.RemoveAt(i);
            }
            RefreshFavorite();
        }

        public BatchFunction[] TemplateFunctions { get; private set; } = new BatchFunction[]
        {
            new FunctionChangeFormat(),
            new FunctionMove(),
            new FunctionReplace()
        };
        public BatchFunction[] FavoriteFunctions
        {
            get
            {
                List<BatchFunction> list = new List<BatchFunction>();
                foreach (BatchFunction item in Functions)
                {
                    if (item.IsFavorite) list.Add(item);
                }
                return list.ToArray();
            }
        }
        public ObservableCollection<BatchFunction> Functions { get; private set; } = new ObservableCollection<BatchFunction>();
    }
}
