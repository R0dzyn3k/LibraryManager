using System.Collections.Generic;
using System.Linq;
using LibraryManager.Data;
using LibraryManager.Models;

namespace LibraryManager.Services;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _context;

    public BookRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public List<Book> GetAllBooks()
    {
        return _context.Books.ToList();
    }

    public Book? GetBookById(int id)
    {
        return _context.Books.FirstOrDefault(b => b.Id == id);
    }

    public bool SaveBook(Book book)
    {
        if (book.Id == 0)
        {
            _context.Books.Add(book);
        }
        else
        {
            var existingBook = _context.Books.FirstOrDefault(b => b.Id == book.Id);
            if (existingBook == null) return false;

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Publisher = book.Publisher;
            existingBook.TotalCount = book.TotalCount;
            existingBook.Summary = book.Summary;
            existingBook.Genre = book.Genre;
        }

        _context.SaveChanges();
        return true;
    }

    public void DeleteBook(int id)
    {
        var book = _context.Books.FirstOrDefault(b => b.Id == id);
        if (book != null)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}