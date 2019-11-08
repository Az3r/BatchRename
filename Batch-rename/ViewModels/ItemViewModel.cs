using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BatchRename.Models;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Data;
using System.Globalization;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using BatchRename.DataTypes;
namespace BatchRename.ViewModels
{
    public class ItemViewModel
    {
        public ItemViewModel() 
        {
            Actions.Add(new BatchFunction());
        }

        /* 
         * Private Helpers
         */
         private void AddItems(string[] collection)
        {
            // must be no duplicate
            foreach (string path in collection)
            {
                BatchItem item = new BatchItem();
                if (BatchPath.IsDirectory(path)) item.Target = new BatchDirectory(path);
                if (BatchPath.IsFile(path)) item.Target = new BatchFile(path);
                item.TargetDisplay = mFullDisplay ? item.TargetFullName : item.TargetName;
                Items.Add(item);
            }
        }

        /*
         * Interfaces
         */
        public void AddFileFromExplorer()
        {
            using (OpenFileDialog dialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = true
            })
            {

                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK) AddItems(dialog.FileNames);
            };
        }
        public void AddDraggedItems(string[] collection)
        {
            AddItems(collection);
        }
        public void RemoveItems(IList collection)
        {
            while (collection.Count > 0)
                Items.Remove(collection[0] as BatchItem);
        }
        /*
         * Properties
         */
        public bool? FullDisplay
        {
            get => mFullDisplay;
            set
            {
                mFullDisplay = false;
                if (value.HasValue && value.Value == true) mFullDisplay = true;
                foreach (BatchItem item in Items)
                {
                    item.TargetDisplay = mFullDisplay ? item.TargetFullName : item.TargetName;
                    item.ResultDisplay = mFullDisplay ? item.ResultFullName : item.ResultName;
                }
            }
        }
        public BatchFunction[] TemplateFunctions { get; private set; } = new BatchFunction[]
        {
            new FunctionChangeFormat(),
            new FunctionMove(),
            new FunctionReplace(),
        };
        public ObservableHashSet<BatchItem> Items { get; set; } = new ObservableHashSet<BatchItem>();
        public ObservableCollection<BatchFunction> Actions { get; set; } = new ObservableCollection<BatchFunction>();

        private bool mFullDisplay;
    }
}
