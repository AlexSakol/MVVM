using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace WPF_LAB_9.ViewModels
{
    public class ViewModelBase: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       
        protected bool Set<T>(ref T prop, T value, [CallerMemberName] string propName = null)
        {
            if (Equals(prop, value)) return false;
            prop = value;
            OnPropertyChanged(propName);
            return true;
        }
    }
}
