using System;
using System.ComponentModel;
using GalaSoft.MvvmLight.CommandWpf;
using LibraryManager.Data;
using LibraryManager.Models;
using LibraryManager.Services;
using LibraryManager.View;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.ViewModel;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly AuthService _authService;
    private readonly BookService _bookService;
    private readonly LibraryDbContext _dbContext;
    private object _currentView;
    private bool _isAdmin;
    private bool _isLoggedIn;

    public MainWindowViewModel()
    {
        _dbContext = new LibraryDbContext();
        _dbContext.Database.Migrate();

        _authService = new AuthService(_dbContext);
        _authService.PropertyChanged += AuthServiceOnPropertyChanged;
        _bookService = new BookService(new BookRepository(_dbContext));

        LogoutCommand = new RelayCommand(Logout);
        MyAccountCommand = new RelayCommand(MyAccount);
        BooksCommand = new RelayCommand(ShowBooks);

        LoginViewModel = new LoginViewModel(_authService);
        LoginViewModel.LoginSuccess += OnLoginSuccess;
        LoginViewModel.NavigateToRegister += OnNavigateToRegister;

        RegisterViewModel = new RegisterViewModel(_authService);
        RegisterViewModel.NavigateToLogin += OnNavigateToLogin;
        RegisterCommand = new RelayCommand(() => CurrentView = RegisterViewModel);

        AddBookCommand = new RelayCommand(AddBook);

        DashboardCommand = new RelayCommand(ShowDashboard);

        CurrentView = LoginViewModel;
    }

    public RelayCommand LogoutCommand { get; private set; }

    public RelayCommand MyAccountCommand { get; private set; }

    public RelayCommand DashboardCommand { get; private set; }

    public LoginViewModel LoginViewModel { get; set; }

    public RegisterViewModel RegisterViewModel { get; set; }

    public RelayCommand RegisterCommand { get; private set; }

    public RelayCommand AddBookCommand { get; private set; }

    public RelayCommand BooksCommand { get; private set; }

    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged(nameof(CurrentView));
        }
    }

    public bool IsLoggedIn
    {
        get => _isLoggedIn;
        private set
        {
            _isLoggedIn = value;
            OnPropertyChanged(nameof(IsLoggedIn));
        }
    }

    public bool IsAdmin
    {
        get => _isAdmin;
        private set
        {
            _isAdmin = value;
            OnPropertyChanged(nameof(IsAdmin));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void OnLoginSuccess(object sender, EventArgs e)
    {
        var dashboardView = new DashboardView();
        dashboardView.DataContext = new DashboardViewModel(_authService);
        CurrentView = dashboardView;
    }

    private void OnNavigateToRegister()
    {
        CurrentView = RegisterViewModel;
    }

    private void OnNavigateToLogin()
    {
        CurrentView = LoginViewModel;
    }

    private void Logout()
    {
        _authService.Logout();
        CurrentView = LoginViewModel;
    }

    private void AuthServiceOnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AuthService.IsLoggedIn))
        {
            IsLoggedIn = _authService.IsLoggedIn;
            OnPropertyChanged(nameof(IsLoggedIn));
            UpdateIsAdmin();
        }
    }

    private void UpdateIsAdmin()
    {
        IsAdmin = _authService.UserRole == Role.Admin;
    }

    private void MyAccount()
    {
        var user = _authService.GetCurrentUser();

        if (IsLoggedIn && user != null)
        {
            var userView = new UserView();
            userView.DataContext = user;
            CurrentView = userView;
        }
        else
        {
            CurrentView = LoginViewModel;
        }
    }

    private void ShowDashboard()
    {
        var dashboardView = new DashboardView();
        dashboardView.DataContext = new DashboardViewModel(_authService);
        CurrentView = dashboardView;
    }

    private void AddBook()
    {
        var bookEditView = new BookEditView();
        var bookEditViewModel = new BookEditViewModel(_dbContext, _bookService);
        bookEditViewModel.BookSaved += NavigateToBookDetail;
        bookEditView.DataContext = bookEditViewModel;
        CurrentView = bookEditView;
    }

    private void ShowBooks()
    {
        var bookListView = new BookListView();
        var bookListViewModel = new BookListViewModel(_bookService, _authService);
        bookListViewModel.NavigateToBookDetails += NavigateToBookDetail;
        bookListViewModel.NavigateToBookEdit += NavigateToBookEdit;
        bookListView.DataContext = bookListViewModel;
        CurrentView = bookListView;
    }

    public void NavigateToBookDetail(int id)
    {
        var bookDetailView = new BookDetailView();
        var bookDetailViewModel = new BookDetailViewModel(_bookService, id);
        bookDetailView.DataContext = bookDetailViewModel;
        CurrentView = bookDetailView;
    }

    private void NavigateToBookEdit(int id)
    {
        var bookEditView = new BookEditView();
        var bookEditViewModel = new BookEditViewModel(_dbContext, _bookService, id);
        bookEditViewModel.BookSaved += NavigateToBookDetail;
        bookEditView.DataContext = bookEditViewModel;
        CurrentView = bookEditView;
    }
}