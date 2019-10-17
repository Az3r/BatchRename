using BatchRename.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BatchRename.ViewModel
{
    public class FileViewModel : INotifyPropertyChanged
    {
        public FileViewModel() { }
        public FileViewModel(PathInfo path, BaseAction action)
        {
            target = path;
            Action = action;
        }

        public static FileViewModel[] CreateArray(PathInfo[] collection, BaseAction action)
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
            bool success = target.Move(preview.FullName, out string error);
            if (success) Description = "Successfully";
            else Description = error;
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
        public string TxtOldName => target.GetName();
        public string TxtNewName => preview?.FullName;
        private BaseAction Action 
        {
            get => action;
            set
            {
                action = value;
                preview = action?.Execute(target);
                //NotifyPropertyChanged(nameof(TxtNewName));
            }
        }
        public PathInfo Target
        {
            get => target;
            set
            {
                target = value;
                preview = action?.Execute(target);
            }
        }
        public PathInfo Preview => preview;

        private string sLogger;
        private PathInfo target;
        private PathInfo preview;
        private BaseAction action = new BaseAction();
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
