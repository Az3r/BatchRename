using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace BatchRename.Shared
{
    /// <summary>
    /// Implement <see cref="INotifyPropertyChanged"/>, this class is <see langword="abstract"/>
    /// </summary>
    public abstract class EventNotifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propery = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propery)); }
    }
}
