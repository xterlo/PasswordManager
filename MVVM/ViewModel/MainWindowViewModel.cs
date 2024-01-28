using PasswordManager.Core;


namespace PasswordManager.MVVM.ViewModel
{
    internal class MainWindowViewModel : ObserverObject
    {
        private string _Title { get; set; } = "test1";
        public string Title
        {
            get => _Title;
            set { _Title = value; OnPropertyChanged(nameof(Title)); }
        }
    }
}
