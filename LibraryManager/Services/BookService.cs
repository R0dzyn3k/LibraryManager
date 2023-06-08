using System.Collections.Generic;
using LibraryManager.Models;

namespace LibraryManager.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public List<Book> GetAllBooks()
    {
        return _bookRepository.GetAllBooks();
    }

    public Book? GetBookById(int id)
    {
        return _bookRepository.GetBookById(id);
    }

    public bool SaveBook(Book book)
    {
        return _bookRepository.SaveBook(book);
    }

    public void DeleteBook(int id)
    {
        _bookRepository.DeleteBook(id);
    }

    public List<Genre> GetAvailableGenres()
    {
        return new List<Genre>
        {
            Genre.Action,
            Genre.Adventure,
            Genre.Comedy,
            Genre.Crime,
            Genre.Drama,
            Genre.Fantasy,
            Genre.HistoricalFiction,
            Genre.Horror,
            Genre.Mystery,
            Genre.Romance,
            Genre.ScienceFiction,
            Genre.Thriller,
            Genre.Biography,
            Genre.Autobiography,
            Genre.Poetry,
            Genre.SelfHelp,
            Genre.Business,
            Genre.History,
            Genre.Philosophy,
            Genre.Travel
        };
    }
}