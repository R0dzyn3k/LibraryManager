using System.ComponentModel;
using LibraryManager.Models;
using LibraryManager.Services;
using PropertyChanged;

namespace LibraryManager.ViewModel;

[AddINotifyPropertyChangedInterface]
public class BookDetailViewModel : INotifyPropertyChanged
{
    private Book _book;

    public BookDetailViewModel(BookService bookService, int bookId)
    {
        _book = bookService.GetBookById(bookId) ?? new Book();
    }

    public Book Book
    {
        get => _book;
        set
        {
            _book = value;
            OnPropertyChanged(nameof(Book));
        }
    }

    public string Title
    {
        get => Book.Title;
        set
        {
            Book.Title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    public string Author
    {
        get => Book.Author;
        set
        {
            Book.Author = value;
            OnPropertyChanged(nameof(Author));
        }
    }

    public string Publisher
    {
        get => Book.Publisher;
        set
        {
            Book.Publisher = value;
            OnPropertyChanged(nameof(Publisher));
        }
    }

    public int Count
    {
        get => Book.AvailableCount;
        set
        {
            Book.AvailableCount = value;
            OnPropertyChanged(nameof(Count));
        }
    }

    public string Summary
    {
        get => Book.Summary;
        set
        {
            Book.Summary = value;
            OnPropertyChanged(nameof(Summary));
        }
    }

    public Genre SelectedGenre
    {
        get => Book.Genre;
        set
        {
            Book.Genre = value;
            OnPropertyChanged(nameof(SelectedGenre));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}