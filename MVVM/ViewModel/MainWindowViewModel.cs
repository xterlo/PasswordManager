using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using PasswordManager.Core;
using PasswordManager.Models;


namespace PasswordManager.MVVM.ViewModel
{
    internal class MainWindowViewModel : ObserverObject
    {
        private RegistryKey currentUserKey;
        private RegistryKey JWTKey;

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


        private void GetSettings()
        {
            Registry.CurrentUser.CreateSubKey("PasswordManager", true);
            currentUserKey = Registry.CurrentUser.OpenSubKey("PasswordManager", true);
            JWTKey = currentUserKey.CreateSubKey("Settings");
            if (JWTKey.GetValue("JWT") is null) JWTKey.SetValue("JWT", "");
            if (JWTKey.GetValue("UserName") is null) JWTKey.SetValue("UserName", "");
            Settings.JWT = JWTKey.GetValue("JWT").ToString();
            Settings.UserName = JWTKey.GetValue("UserName").ToString();
        }

        private async void Validate()
        {
            var response = await ResponseHandler.SendValidate(Settings.JWT);
            if (response.status) CurrentView = new HomeWindowViewModel();
            else CurrentView = new AuthWindowViewModel();
            

        }

        public MainWindowViewModel()
        {
            GetSettings();
            Validate();


        }
    }
}
