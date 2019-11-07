using BatchRename.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatchRename.DataTypes;
namespace BatchRename.Models
{
    public class BatchItem : EventNotifier, IEquatable<BatchItem>
    {
        public BatchItem() { }

        public bool Equals(BatchItem other)
        {
            if (Target == null || other == null) return false;
            return Target.Equals(other.Target);
        }
        static public bool Equals(BatchItem x, BatchItem y)
        {
            return x.Equals(y);
        }

        // The string to be displayed, either full name or name only
        public string TargetDisplay
        {
            get => mTargetDisplay;
            set
            {
                mTargetDisplay = value;
                NotifyPropertyChanged();
            }
        }
        // The string to be displayed, either full name or name only
        public string ResultDisplay
        {
            get => mResultDisplay;
            set
            {
                mResultDisplay = value;
                NotifyPropertyChanged();
            }
        }
        public string Message { get; private set; } = string.Empty;
        public string TargetName => Target?.Name;
        public string TargetFullName => Target?.FullName;
        public string ResultName => Result?.Name;
        public string ResultFullName => Result?.FullName;
        public bool IsGood { get; private set; } = true;
        public BatchFunction[] Actions { get; set; } = new BatchFunction[0];
        public BatchPath Target { get; set; }
        public BatchPath Result { get; set; }

        private string mTargetDisplay;
        private string mResultDisplay;
    }
}
