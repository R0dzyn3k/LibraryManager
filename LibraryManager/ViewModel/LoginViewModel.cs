using System;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using LibraryManager.Services;
using PropertyChanged;

namespace LibraryManager.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class LoginViewModel
    {
        private readonly IAuthService _authService;
        private RelayCommand _loginCommand;

        private string? _username;

        public string? Username
        {
            get => _username;
            set
            {
                _username = value;
                _loginCommand.RaiseCanExecuteChanged();
            }
        }

        private string? _password;

        public string? Password
        {
            get => _password;
            set
            {
                _password = value;
                _loginCommand.RaiseCanExecuteChanged();
            }
        }

        public ICommand LoginCommand => _loginCommand;

        public string? ErrorMessage { get; private set; }

        public event EventHandler? LoginSuccess;

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
            _loginCommand = new RelayCommand(Login, true);
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(Username) || Username.Length < 3 ||
                string.IsNullOrEmpty(Password) || Password.Length < 3)
            {
                ErrorMessage = "Username and password should contain at least 3 characters.";
                return;
            }

            var success = await _authService.LoginAsync(Username, Password);

            if (success)
            {
                LoginSuccess?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                ErrorMessage = "Invalid login credentials.";
                
            }
        }
    }
}
