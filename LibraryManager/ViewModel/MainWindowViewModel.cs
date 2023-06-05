using System;
using System.ComponentModel;
using GalaSoft.MvvmLight.CommandWpf;
using LibraryManager.Data;
using LibraryManager.Services;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.ViewModel;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly LibraryDbContext _dbContext;

    private object _currentView;

    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged(nameof(CurrentView));
        }
    }

    public LoginViewModel LoginViewModel { get; set; }

    public RegisterViewModel RegisterViewModel { get; set; }

    public RelayCommand RegisterCommand { get; private set; }

    public event PropertyChangedEventHandler PropertyChanged;

    public MainWindowViewModel()
    {
        _dbContext = new LibraryDbContext();
        _dbContext.Database.Migrate();

        var authService = new AuthService(_dbContext);

        LoginViewModel = new LoginViewModel(authService);
        LoginViewModel.LoginSuccess += OnLoginSuccess;
        LoginViewModel.NavigateToRegister += OnNavigateToRegister;
        
        RegisterViewModel = new RegisterViewModel(authService);
        RegisterViewModel.NavigateToLogin += OnNavigateToLogin;

        RegisterCommand = new RelayCommand(() => CurrentView = RegisterViewModel);

        CurrentView = LoginViewModel;
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void OnLoginSuccess(object sender, EventArgs e)
    {
        // Przełącz na inną stronę po pomyślnym zalogowaniu
        // CurrentView = new SomeOtherViewModel();
    }
    private void OnNavigateToRegister()
    {
        CurrentView = RegisterViewModel;
    }
    
    private void OnNavigateToLogin()
    {
        CurrentView = LoginViewModel;
    }
    
}