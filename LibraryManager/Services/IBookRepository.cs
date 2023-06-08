using System.Collections.Generic;
using LibraryManager.Models;

namespace LibraryManager.Services;

public interface IBookRepository
{
    List<Book> GetAllBooks();
    Book? GetBookById(int id);
    bool SaveBook(Book book);
    void DeleteBook(int id);
}