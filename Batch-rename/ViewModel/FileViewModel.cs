using BatchRename.Model;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;

namespace BatchRename.ViewModel
{
    public class FileViewModel : INotifyPropertyChanged
    {
        public FileViewModel(BatchFile file, FileAction action)
        {
            source = file;
            Action = action;
        }

        public static FileViewModel[] CreateArray(BatchFile[] collection, FileAction action)
        {
            FileViewModel[] array = new FileViewModel[collection.Length];
            for (int i = 0; i < collection.Length; ++i)
                array[i] = new FileViewModel(collection[i], action);
            return array;
        }

        protected void NotifyPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public void Rename(bool overwrite)
        {
            BatchFile result = Action?.Execute(source);
            if (result.Exists && overwrite)
            {
                if (result.IsFile) result.DeleteFile();
                else result.DeleteDirectory(overwrite);
            }
            else if(!result.Exists)
            {
                result.Create();
                if (source.IsFile) source.DeleteFile();
                else source.DeleteDirectory(true);
            }
            System.IO.mo
        }

        private static FileSystemSecurity GetAccessControl(BatchFile sf)
        {
            if (sf.IsFile) return File.GetAccessControl(sf.FullPath);
            else return File.GetAccessControl(sf.FullPath);
        }

        public string Description
        {
            get => sLogger;
            private set
            {
                sLogger = value;
                NotifyPropertyChanged();
            }
        }
        public string TxtOldName => source.Name;
        public string TxtNewName
        {
            get
            {
                if (Action != null)
                {
                    BatchFile preview = new BatchFile(source.FullPath);
                    return Action.Execute(preview).Name;
                }
                return string.Empty;
            }
        }
        private FileAction Action 
        {
            get => action;
            set
            {
                action = value;
                NotifyPropertyChanged(nameof(TxtNewName));
            }
        }

        private string sLogger;
        private FileAction action;
        private BatchFile source;
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
