using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatchRename.Models;
namespace BatchRename.ViewModels
{
    public class ActionExplorerViewModel
    {
        public ActionExplorerViewModel()
        {
            Functions.Add(new BatchFunction() { IsFavorite = true, Name = "Fucntion" });
            Functions.Add(new BatchFunction() { IsFavorite = false, Name = "Fucntion" });

            RefreshFavorite();
        }

        public void RefreshFavorite()
        {
            FavoriteFunctions.Clear();
            foreach (BatchFunction item in Functions)
            {
                if (item.IsFavorite) FavoriteFunctions.Add(item);
            }
        }
        public void CreateFunction()
        {
            Functions.Add(new BatchFunction() { IsFavorite = false, Name = "Fucntion" });
        }

        public ObservableCollection<BatchFunction> FavoriteFunctions { get; private set; } = new ObservableCollection<BatchFunction>();
        public ObservableCollection<BatchFunction> Functions { get; private set;} = new ObservableCollection<BatchFunction>();
    }
}
