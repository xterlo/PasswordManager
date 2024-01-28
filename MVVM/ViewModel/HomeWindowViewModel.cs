using Microsoft.Win32;
using PasswordManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static PasswordManager.Core.ResponseHandler;

namespace PasswordManager.MVVM.ViewModel
{
    internal class HomeWindowViewModel : ObserverObject
    {

        private string _UserName { get; set; } = Settings.UserName;
        public string UserName
        {
            get => _UserName;
            set
            {
                _UserName = value;
                OnPropertyChanged(nameof(UserName));
            }
        } 
        
        private ICommand _LogoutCommand { get; set; }
        public ICommand LogoutCommand
        {
            get => _LogoutCommand;
            set
            {
                _LogoutCommand = value;
                OnPropertyChanged(nameof(LogoutCommand));
            }
        }

        public HomeWindowViewModel()
        {
            LogoutCommand = new RelayCommand(() =>
            {
                var currentUserKey = Registry.CurrentUser.OpenSubKey("PasswordManager", true);
                var JWTKey = currentUserKey.CreateSubKey("Settings");
                JWTKey.SetValue("JWT", "");
                JWTKey.SetValue("UserName", "");
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            });
        }
    }
}
