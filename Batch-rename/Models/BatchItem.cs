using BatchRename.DataTypes;
using BatchRename.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BatchRename.Models
{
    public class BatchItem : EventNotifier, IEquatable<BatchItem>
    {
        public BatchItem() { }

        /// <summary>
        /// Apply functions in <see cref="Actions"/> to <see cref="Target"/>, the result will be saved in <see cref="Result"/> 
        /// <para>Note: No file io operation is involved</para>
        /// </summary>
        public async Task Commit()
        {
            if (Actions == null || Actions.GetEnumerator().MoveNext() == false) return;
            Task task = Task.Run(() =>
            {
                string newName = Target.Name;
                foreach (BatchFunction function in Actions)
                {
                    newName = function.GetString(newName);
                }
                string newPath = Path.Combine(Target.GetParent(), newName);
                Result = Target.Clone();
                Result.FullName = newPath;
            });
            await task;
        }
        /// <summary>
        /// Get the result in <see cref="Result"/> and start renaming file or folder
        /// </summary>
        public async Task Rename()
        {
            if (Result == null || Result.FullName == string.Empty) return;
            Message = "Running...";
            Task task = Task.Run(() =>
            {
                fallBackTarget = Target.Clone();
                int err = Target.Delete();
                if (!Error.IsGood(err)) { Message = Error.GetMessage(err); return; }

                err = Result.Create(IsOverWrite);
                if (!Error.IsGood(err))
                {
                    fallBackTarget.Create(true);
                    Target = fallBackTarget;
                    Message = Error.GetMessage(err);
                    return;
                }
                Target.FullName = Result.FullName;
                Result.FullName = string.Empty;
                Result = null;
                Message = "Sucessfully";
            });
            await task;
        }
        public bool Equals(BatchItem other)
        {
            if (Target == null || other == null) return false;
            return Target.Equals(other.Target);
        }
        static public bool Equals(BatchItem x, BatchItem y)
        {
            return x.Equals(y);
        }
        public bool IsOverWrite
        {
            get => mOverWrite;
            set
            {
                mOverWrite = value;
                NotifyPropertyChanged();
            }
        }
        public string Message { get; private set; } = string.Empty;
        public IEnumerable<BatchFunction> Actions { get; set; }
        public BatchPath Target { get; set; }
        public BatchPath Result { get; set; }

        private BatchPath fallBackTarget;
        private bool mOverWrite;
    }
}
