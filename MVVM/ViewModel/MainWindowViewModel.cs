using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Input;
using System.Windows.Media;
using PasswordManager.Core;
using PasswordManager.Models;


namespace PasswordManager.MVVM.ViewModel
{
    internal class MainWindowViewModel : ObserverObject
    {
        private string _Title { get; set; } = "Password Manager";
        public string Title
        {
            get => _Title;
            set { _Title = value; OnPropertyChanged(nameof(Title)); }
        }

        private object _CurrentView { get; set; }
        public object CurrentView
        {
            get => _CurrentView;
            set
            {
                _CurrentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }


        public MainWindowViewModel()
        {
            CurrentView = new AuthWindowViewModel();

        }
    }
}
