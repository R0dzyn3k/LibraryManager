using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public Role Role { get; set; }

    public User(string username, string password, string firstName, string lastName, string email, string phone)
    {
        Username = username;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Role = Role.Guest;
    }
}