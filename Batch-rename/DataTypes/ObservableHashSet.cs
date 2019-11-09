using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace BatchRename.DataTypes
{
    public class ObservableHashSet<T> : ObservableCollection<T>
    {
        public ObservableHashSet() : base() { }
        public ObservableHashSet(IEnumerable<T> collection) : base()
        {
            watcher = new HashSet<T>(collection);
            foreach (T item in watcher)
            {
                base.Add(item);
            }
        }
        public new void Add(T item)
        {
            if (watcher.Add(item)) base.Add(item);
        }
        public void AddRange(IEnumerable<T> collection)
        {
            foreach (T item in collection) Add(item);
        }
        public void Remove(T item)
        {
            watcher.Remove(item);
            base.Remove(item);
        }
        private HashSet<T> watcher = new HashSet<T>();
    }
}
