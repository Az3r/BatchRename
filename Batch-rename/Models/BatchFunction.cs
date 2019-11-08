using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BatchRename.Shared;
using BatchRename.DataTypes;
using System.Runtime.Serialization;
using System.Windows;

namespace BatchRename.Models
{
    [Serializable]
    public class BatchFunction : EventNotifier, ISerializable
    {
        public BatchFunction() { }
        protected BatchFunction(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");
            Name = info.GetString(nameof(Name));
        }
        public virtual BatchPath GetPath(BatchPath path)
        {
            BatchPath clone = path.Clone();
            clone.FullName = GetString(path.FullName);
            return clone;
        }
        public virtual string GetString(string src) { return src; }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");
            info.AddValue(nameof(Name), Name);
        }

        public string Name
        {
            get => mName;
            set
            {
                mName = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsFavorite
        {
            get => mFavorite;
            set
            {
                mFavorite = value;
                NotifyPropertyChanged();
            }
        }

        private string mName = string.Empty;
        private bool mFavorite = false;
    }
}
