using Microsoft.Win32;
using PasswordManager.Core;
using PasswordManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace PasswordManager.MVVM.ViewModel
{
    internal class AuthWindowViewModel : ObserverObject
    {
        private string _Title { get; set; } = "Password Manager";
        public string Title
        {
            get => _Title;
            set { _Title = value; OnPropertyChanged(nameof(Title)); }
        }

        private string _Status { get; set; }
        public string Status
        {
            get => _Status;
            set { _Status = value; OnPropertyChanged(nameof(Status)); }
        }

        private SolidColorBrush _StatusColor { get; set; } = Brushes.Red;
        public SolidColorBrush StatusColor
        {
            get => _StatusColor;
            set { _StatusColor = value; OnPropertyChanged(nameof(StatusColor)); }
        }

        private string _Login { get; set; }
        public string Login
        {
            get => _Login;
            set { _Login = value; OnPropertyChanged(nameof(Login)); }
        }

        private string _Password { get; set; }
        public object Password
        {
            get => _Password;
            set { _Password = ConvertToUnsecureString(value as SecureString); OnPropertyChanged(nameof(Password)); }
        }

        private ICommand _AuthCommand { get; set; }
        public ICommand AuthCommand
        {
            get => _AuthCommand;
            set => _AuthCommand = value;
        }

        private string ConvertToUnsecureString(SecureString secureString)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                string regularString = Marshal.PtrToStringUni(unmanagedString);
                return regularString;
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        private async void SendAuth()
        {
            ResponseModel<AuthData> response = await ResponseHandler.SendAuth(Login, Password as string);
            Status = response.message;
            if (!response.status)
            {
                StatusColor = Brushes.Red;
            }
            else
            {
                var b = response.data;
                StatusColor = Brushes.Green;
                var currentUserKey = Registry.CurrentUser.OpenSubKey("PasswordManager", true);
                var JWTKey = currentUserKey.CreateSubKey("Settings");
                JWTKey.SetValue("JWT", response.data.session);
                JWTKey.SetValue("UserName", response.data.name);
            }

        }



        public AuthWindowViewModel()
        {
            AuthCommand = new RelayCommand(SendAuth);
        }
    }
}
