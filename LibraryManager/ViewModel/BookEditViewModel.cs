using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using LibraryManager.Models;
using LibraryManager.Services;
using LibraryManager.Validators;
using PropertyChanged;

namespace LibraryManager.ViewModel;

[AddINotifyPropertyChangedInterface]
public class BookEditViewModel : INotifyPropertyChanged
{
    private readonly AuthorValidator _authorValidator = new();
    private readonly IBookService _bookService;
    private readonly RelayCommand _clearCommand;
    private readonly CountValidator _countValidator = new();
    private readonly PublisherValidator _publisherValidator = new();
    private readonly RelayCommand _saveBookCommand;
    private readonly TitleValidator _titleValidator = new();
    private string? _author;
    private int _count;
    private string? _imagePath;
    private string? _publisher;
    private Genre _selectedGenre;
    private string? _summary;
    private string? _title;

    public BookEditViewModel(IBookService bookService)
    {
        _bookService = bookService;
        _saveBookCommand = new RelayCommand(SaveBook, CanSaveBook);
        _clearCommand = new RelayCommand(Clear);

        AvailableGenres = _bookService.GetAvailableGenres();
    }

    public string? Title
    {
        get => _title;
        set => SetProperty(ref _title, value, nameof(Title));
    }

    public string? Author
    {
        get => _author;
        set => SetProperty(ref _author, value, nameof(Author));
    }

    public string? Publisher
    {
        get => _publisher;
        set => SetProperty(ref _publisher, value, nameof(Publisher));
    }

    public int Count
    {
        get => _count;
        set => SetProperty(ref _count, value, nameof(Count));
    }

    public string? Summary
    {
        get => _summary;
        set => SetProperty(ref _summary, value, nameof(Summary));
    }

    public Genre SelectedGenre
    {
        get => _selectedGenre;
        set => SetProperty(ref _selectedGenre, value, nameof(SelectedGenre));
    }

    public string? ImagePath
    {
        get => _imagePath;
        set => SetProperty(ref _imagePath, value, nameof(ImagePath));
    }

    public List<Genre> AvailableGenres { get; }

    public ICommand SaveBookCommand => _saveBookCommand;
    public ICommand ClearCommand => _clearCommand;

    public MessageType MessageType { get; private set; }
    public string? Message { get; private set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetProperty<T>(ref T field, T value, string propertyName)
    {
        if (!EqualityComparer<T>.Default.Equals(field, value))
        {
            field = value;
            OnPropertyChanged(propertyName);
        }
    }

    private bool CanSaveBook()
    {
        var titleValidation = _titleValidator.Validate(Title);
        if (!titleValidation.IsValid)
        {
            SetErrorMessage(titleValidation.ErrorMessage);
            return false;
        }

        var authorValidation = _authorValidator.Validate(Author);
        if (!authorValidation.IsValid)
        {
            SetErrorMessage(authorValidation.ErrorMessage);
            return false;
        }

        var publisherValidation = _publisherValidator.Validate(Publisher);
        if (!publisherValidation.IsValid)
        {
            SetErrorMessage(publisherValidation.ErrorMessage);
            return false;
        }

        var countValidation = _countValidator.Validate(Count);
        if (!countValidation.IsValid)
        {
            SetErrorMessage(countValidation.ErrorMessage);
            return false;
        }

        if (string.IsNullOrEmpty(Summary))
        {
            SetErrorMessage("Summary is required.");
            return false;
        }

        if (SelectedGenre == 0)
        {
            SetErrorMessage("Genre is required.");
            return false;
        }

        ClearErrorMessage();
        return true;
    }

    private void SaveBook()
    {
        var newBook = new Book(Title!, Author!, Publisher!, Count, SelectedGenre, Summary!, ImagePath);

        var success = _bookService.SaveBook(newBook);

        if (success)
        {
            SetMessage(MessageType.Success, "Book saved successfully.");
            BookSaved?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            SetMessage(MessageType.Error, "Failed to save the book.");
        }
    }

    private void Clear()
    {
        Title = null;
        Author = null;
        Publisher = null;
        Count = 0;
        Summary = null;
        SelectedGenre = 0;
        ImagePath = null;

        ClearForm?.Invoke();
    }

    private void SetErrorMessage(string message)
    {
        MessageType = MessageType.Error;
        Message = message;
    }

    private void SetMessage(MessageType messageType, string message)
    {
        MessageType = messageType;
        Message = message;
    }

    private void ClearErrorMessage()
    {
        MessageType = MessageType.None;
        Message = null;
    }

    public event EventHandler? BookSaved;
    public event Action? ClearForm;
}