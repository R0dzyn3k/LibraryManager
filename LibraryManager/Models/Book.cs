using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models;

public class Book
{
    public Book(string title, string author, string publisher, int count, Genre genre, string summary,
        string? imagePath)
    {
        Title = title;
        Author = author;
        Publisher = publisher;
        Genre = genre;
        Summary = summary;
        ImagePath = imagePath;
        TotalCount = count;
        AvailableCount = count;
    }

    [Key] public int Id { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public string Publisher { get; set; }
    public Genre Genre { get; set; }

    public string Summary { get; set; }

    public string? ImagePath { get; set; }

    public int TotalCount { get; set; }

    public int AvailableCount { get; set; }
}