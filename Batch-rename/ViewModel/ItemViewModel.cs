using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BatchRename.Model;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Data;
using System.Globalization;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using BatchRename.ViewModel.Command;
namespace BatchRename.ViewModel
{
    public class ItemViewModel
    {
        public ItemViewModel() { }
        public void AddFileFromExplorder()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = true
            };

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                int length = ItemCollection.Count;

                foreach (string path in dialog.FileNames)
                {
                    // Add file into collection if it doesn't currently exist 
                    bool bExists = false;
                    for (int i = 0; i < length; ++i)
                    {
                        if (ItemCollection[i].GetTargetFullName().Equals(path, StringComparison.Ordinal))
                        {
                            bExists = true;
                            break;
                        }
                    }
                    if (!bExists)
                    {
                        BatchItem item = new BatchItem();
                        item.SetTarget(new BatchFile(path));
                        ItemCollection.Add(item);
                    }
                }
            }
        }
        public void AddDraggedItems(string[] collection)
        {
            foreach (string path in collection)
            {
                if (Directory.Exists(path))
                {
                    BatchItem item = new BatchItem();
                    item.SetTarget(new BatchDirectory(path));
                    ItemCollection.Add(item);
                }
                else if (File.Exists(path))
                {
                    BatchItem item = new BatchItem();
                    item.SetTarget(new BatchFile(path));
                    ItemCollection.Add(item);
                }
            }
        }
        public void RemoveSelectedItems()
        {
            for (int i = 0; i < SelectedItems.Count; ++i)
                ItemCollection.Remove(SelectedItems[i] as BatchItem);
        }
        public bool? FullDisplay
        {
            get => mFullDisplay;
            set
            {
                mFullDisplay = value;
                bool bFull = mFullDisplay.HasValue && mFullDisplay.Value == true;
                foreach (BatchItem item in ItemCollection)
                    item.ShowFullName = bFull;
            }
        }
        public IList SelectedItems { get; set; }
        public ObservableCollection<BatchItem> ItemCollection { get; set; } = new ObservableCollection<BatchItem>();
        public ObservableCollection<BatchFunction> SelectedActions { get; set; } = new ObservableCollection<BatchFunction>();
        public ItemExecuteSelectedCommand ExecuteSelected { get; private set; } = new ItemExecuteSelectedCommand();
        public ItemExecuteAllCommand ExecuteAll { get; private set; } = new ItemExecuteAllCommand();
        public ItemAddCommand AddItem { get; private set; } = new ItemAddCommand();
        public ItemRemoveCommand RemoveItem { get; private set; } = new ItemRemoveCommand();

        private bool? mFullDisplay = false;
    }
}
