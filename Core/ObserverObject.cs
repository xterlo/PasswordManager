using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PasswordManager.Core
{
    internal class ObserverObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
