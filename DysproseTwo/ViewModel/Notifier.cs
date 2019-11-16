using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DysproseTwo.ViewModel
{
    public abstract class Notifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // "CallerMemberName" lets you call this method without having to included the properties name
        // by yourself e.g "NotifyPropertyChanged();"
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
