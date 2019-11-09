using BatchRename.DataTypes;
using BatchRename.Models;
using BatchRename.Shared;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatchRename.ViewModels
{
    public class ItemViewModel : EventNotifier
    {
        public ItemViewModel() { }
        /* 
         * Private Helpers
         */

        private async Task CommitChanges()
        {
            Task task = Task.Run(() =>
            {
                Parallel.ForEach(Items, async (source) =>
                {
                    source.Actions = Functions;
                    await source.Commit();
                });
                RefreshDisplay();
            });
            await task;
        }
        private void Rename(IEnumerable<BatchItem> collection)
        {
            Parallel.ForEach(collection, async (item) =>
            {
                await item.Rename();
            });
        }
        /*
         * Interfaces
         */

        public void RemoveFunctions(IList collection)
        {
            while (collection.Count > 0)
                Functions.Remove(collection[0] as BatchFunction);
        }
        public void AddFunctions(BatchFunction[] functions)
        {
            foreach (BatchFunction function in functions)
            {
                Functions.Add(function);
            }
            CommitChanges();
        }
        public void RenameSelected(IList collection)
        {
            Rename(collection.Cast<BatchItem>());
        }
        public void RenameAll()
        {
            Rename(Items);
        }
        public void RefreshDisplay()
        {
            if (IsFilesAndFolders) ItemDisplayer = AllFilesAndFolders;
            else if (IsFilesOnly) ItemDisplayer = FilesOnly;
            else if (IsFoldersOnly) ItemDisplayer = FoldersOnly;
        }
        public async Task AddFiles(string[] collection)
        {
            Task task = Task.Run(() =>
            {
                foreach (string path in collection)
                {
                    BatchItem item = new BatchItem() { Actions = Functions };
                    if (BatchPath.IsDirectory(path)) item.Target = new BatchDirectory(path);
                    if (BatchPath.IsFile(path)) item.Target = new BatchFile(path);
                    Items.Add(item);
                    item.Commit();
                }
                RefreshDisplay();
            });
            await task;
        }
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
                if (result == DialogResult.OK) AddFiles(dialog.FileNames);
            };
        }
        public void RemoveItems(IList collection)
        {
            foreach (BatchItem item in collection)
            {
                Items.Remove(item);
            }
            RefreshDisplay();
        }
        /*
         * Properties
         */
        public BatchItem[] ItemDisplayer
        {
            get => mItemDisplayer;
            set
            {
                mItemDisplayer = value;
                NotifyPropertyChanged();
            }
        }
        public BatchItem[] FilesOnly
        {
            get
            {
                BatchItem[] array = new BatchItem[Items.Count];
                for (int i = 0; i < Items.Count; i++)
                {
                    if (BatchPath.IsFile(Items[i].Target.FullName))
                        array[i] = Items[i];
                }
                //filter null value
                return array.Where((item) => item != null).ToArray();
            }
        }
        public bool? IsOverWrite
        {
            get => mOverwrite;
            set
            {
                if (value.HasValue && value.Value == true) mOverwrite = true;
                else mOverwrite = false;
            }
        }
        public BatchItem[] FoldersOnly
        {
            get
            {
                BatchItem[] array = new BatchItem[Items.Count];
                for (int i = 0; i < Items.Count; i++)
                {
                    if (BatchPath.IsDirectory(Items[i].Target.FullName))
                        array[i] = Items[i];
                }
                //filter null value
                return array.Where((item) => item != null).ToArray();
            }
        }
        public BatchItem[] AllFilesAndFolders
        {
            get
            {
                BatchItem[] array = new BatchItem[Items.Count];
                Items.CopyTo(array, 0);
                return array;
            }
        }
        public ObservableHashSet<BatchItem> Items { get; set; } = new ObservableHashSet<BatchItem>();
        public ObservableHashSet<BatchFunction> Functions { get; set; } = new ObservableHashSet<BatchFunction>();

        public bool IsFilesOnly { get; set; } = false;
        public bool IsFoldersOnly { get; set; } = false;
        public bool IsFilesAndFolders { get; set; } = true;

        private BatchItem[] mItemDisplayer = new BatchItem[0];
        private bool mOverwrite = false;
    }
}
