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
        [Serializable]
        public delegate string StringDelegate(string target, params object[] args);
        public BatchFunction() { }
        protected BatchFunction(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");
            Name = info.GetString(nameof(Name));
            Handler = info.GetValue(nameof(Handler), typeof(StringDelegate)) as StringDelegate;
            args = info.GetValue(nameof(args), typeof(object[])) as object[];
        }
        public void Invoke(BatchPath target)
        {
            StringDelegate[] delegates = (StringDelegate[])Handler.GetInvocationList();
            string result = target.FullName;
            foreach (StringDelegate func in delegates)
            {
                result = func(result, args);
            }
            target.FullName = result;
        }
        public void Combine(StringDelegate other)
        {
            Handler = (StringDelegate)Delegate.Combine(Handler, other);
        }
        public void Combine(Func<string, object[], string> other)
        {
            Handler = (StringDelegate)Delegate.Combine(Handler, other);
        }
        public void Combine(BatchFunction function)
        {
            Handler = (StringDelegate)Delegate.Combine(Handler, function.Handler);
        }
        public void Combine(StringDelegate[] delegates)
        {
            Handler = (StringDelegate)Delegate.Combine(delegates);
        }
        public void Combine(Func<string, object[], string>[] delegates)
        {
            Handler = (StringDelegate)Delegate.Combine(delegates);
        }
        public void Combine(BatchFunction[] functions)
        {
            StringDelegate[] delegates = new StringDelegate[functions.Length];
            int i = 0;
            foreach (var function in functions)
                delegates[i++] = function.Handler;
            Handler = (StringDelegate)Delegate.Combine(delegates);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");
            info.AddValue(nameof(Name), Name);
            info.AddValue(nameof(Handler), Handler, typeof(StringDelegate));
            info.AddValue(nameof(args), Handler, typeof(object[]));
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
        protected object[] args;
        private string mName = string.Empty;
        private bool mFavorite = false;
        protected StringDelegate Handler = new StringDelegate((s, objs) => s);  // do nothing and return s
    }
}
