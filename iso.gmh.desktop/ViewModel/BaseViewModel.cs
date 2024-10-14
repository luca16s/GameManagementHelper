namespace iso.gmh.desktop.ViewModel;

using System.ComponentModel;
using System.Runtime.CompilerServices;

public partial class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}