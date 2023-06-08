using System.Collections.Generic;
using System.Linq;
using LibraryManager.Models;

namespace LibraryManager.Services;

public class BookRepository : IBookRepository
{
    private readonly List<Book> _books;

    public BookRepository()
    {
        _books = new List<Book>();
    }

    public List<Book> GetAllBooks()
    {
        return _books.ToList();
    }

    public Book? GetBookById(int id)
    {
        return _books.FirstOrDefault(b => b.Id == id) ?? null;
    }

    public bool SaveBook(Book book)
    {
        if (book.Id == 0)
        {
            _books.Add(book);
        }
        else
        {
            var existingBook = _books.FirstOrDefault(b => b.Id == book.Id);
            if (existingBook == null) return false;

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Publisher = book.Publisher;
            existingBook.TotalCount = book.TotalCount;
            existingBook.Summary = book.Summary;
            existingBook.Genre = book.Genre;
        }

        return true;
    }

    public void DeleteBook(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book != null) _books.Remove(book);
    }
}