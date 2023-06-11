using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models;

public class Book
{
    public Book()
    {
    }

    public Book(string title, string author, string publisher, int totalCount, int availableCount, Genre genre,
        string summary)
    {
        Title = title;
        Author = author;
        Publisher = publisher;
        Genre = genre;
        Summary = summary;
        TotalCount = totalCount;
        AvailableCount = availableCount;
    }

    [Key] public int Id { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public string Publisher { get; set; }
    public Genre Genre { get; set; }

    public string Summary { get; set; }

    public int TotalCount { get; set; }

    public int AvailableCount { get; set; }
}