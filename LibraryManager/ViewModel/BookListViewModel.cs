using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using LibraryManager.Models;
using LibraryManager.Services;
using PropertyChanged;

namespace LibraryManager.ViewModel;

[AddINotifyPropertyChangedInterface]
public class BookListViewModel
{
    private readonly IAuthService _authService;
    private readonly BookService _bookService;
    private readonly RelayCommand<Book> _editCommand;
    private readonly RelayCommand<Book> _viewMoreCommand;
    private readonly RelayCommand<Book> _deleteCommand;

    public BookListViewModel(BookService bookService, IAuthService authService)
    {
        _bookService = bookService;
        _authService = authService;

        Books = new ObservableCollection<Book>(_bookService.GetAllBooks());

        _viewMoreCommand = new RelayCommand<Book>(ViewMore);
        _editCommand = new RelayCommand<Book>(Edit, CanEdit);
        _deleteCommand = new RelayCommand<Book>(Delete, CanDelete);
    }

    public ObservableCollection<Book> Books { get; set; }

    public ICommand ViewMoreCommand => _viewMoreCommand;
    public ICommand EditCommand => _editCommand;
    public ICommand DeleteCommand => _deleteCommand;

    public event Action<int> NavigateToBookDetails;
    public event Action<int> NavigateToBookEdit;

    private void ViewMore(Book book)
    {
        NavigateToBookDetails?.Invoke(book.Id);
    }

    private void Edit(Book book)
    {
        NavigateToBookEdit?.Invoke(book.Id);
    }

    private bool CanEdit(Book book)
    {
        var currentUser = _authService.GetCurrentUser();
        return currentUser?.Role == Role.Admin;
    }

    private void Delete(Book book)
    {
        _bookService.DeleteBook(book.Id);
        Books.Remove(book);
    }

    private bool CanDelete(Book book)
    {
        var currentUser = _authService.GetCurrentUser();
        return currentUser?.Role == Role.Admin;
    }
}
