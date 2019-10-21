using BatchRename.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename.Model
{
    public class BatchItem : EventNotifier
    {
        public BatchItem() { }

        private async Task SetResult()
        {
            if (mTarget == null) throw new NullReferenceException($"{nameof(mTarget)} is null");
            if (Action == null) throw new NullReferenceException($"{nameof(Action)} is null");
            if (Action.Length == 0) return;
            Task task = Task.Run(() => 
            {
                // Combine all functions into one
                BatchFunction fusion = new BatchFunction();
                fusion.Combine(Action);

                // Cloning target in start executing
                mResult = mTarget.Clone();
                fusion.Invoke(mResult);

                SetResultDisplay(ShowFullName);
            });
            await task;
        }
        public async Task SetTarget(BatchPath target)
        {
            if (target == null) throw new ArgumentNullException($"{nameof(target)} is null");
            Task task = Task.Run(() =>
            {
                mTarget = target;
                SetTargetDisplay(ShowFullName);

                SetResult();
            });
            await task;
        }
        public async Task SetTarget(string path)
        {
            if (path == null) throw new ArgumentNullException($"{nameof(path)} is null");
            Task task = Task.Run(() =>
            {
                mTarget.SetFullName(path);
                SetTargetDisplay(ShowFullName);
                SetResult();
            });
            await task;
        }
        private void SetTargetDisplay(bool full)
        {
            if (full) TargetName = mTarget.FullName;
            else TargetName = mTarget.Name;
            NotifyPropertyChanged(nameof(TargetName));
        }
        private void SetResultDisplay(bool full)
        {
            if (mResult == null) return;
            if (full) ResultName = mResult.FullName;
            else ResultName = mResult.Name;
            NotifyPropertyChanged(nameof(ResultName));
        }
        public bool ShowFullName
        {
            get => mFullName;
            set
            {
                mFullName = value;
                SetResultDisplay(mFullName);
                SetTargetDisplay(mFullName);
            }
        }
        public string GetTargetFullName() => mTarget?.FullName;
        public string GetResultFullName() => mResult?.FullName;

        public string TargetName { get; private set; } 
        public string ResultName { get; private set; }
        public string Message { get; private set; } = string.Empty;
        public bool IsGood { get; private set; } = true;
        public BatchFunction[] Action { get; set; } = new BatchFunction[0];

        private BatchPath mTarget;
        private BatchPath mResult;
        private bool mFullName = false;
    }
}
