using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using LibraryManager.Data;
using LibraryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Services;

public class AuthService : IAuthService
{
    private readonly LibraryDbContext _context;
    private User? _currentUser;
    private List<User>? _users;

    public AuthService(LibraryDbContext context)
    {
        _context = context;
        InitializeUsers();
    }

    public Role UserRole => _currentUser?.Role ?? Role.Guest;

    private bool _isLoggedIn;

    public bool IsLoggedIn
    {
        get { return _isLoggedIn; }
        set
        {
            if (_isLoggedIn == value) 
                return;
            
            _isLoggedIn = value;
            OnPropertyChanged(nameof(IsLoggedIn));
        }
    }
    
    public event PropertyChangedEventHandler PropertyChanged;

    public async Task<bool> LoginAsync(string? username, string? password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

        if (user == null)
            return false;

        _currentUser = user;
        IsLoggedIn = true;
        return true;
    }

    public async Task<bool> RegisterAsync(User newUser)
    {
        if (_context.Users == null)
            return false;

        try
        {
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            InitializeUsers();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public async Task<bool> CheckUserExistsAsync(string? username)
    {
        if (username == null)
            return false;

        var userExists = await _context.Users.AnyAsync(u => u.Username == username).ConfigureAwait(false);
        
        return userExists;
    }

    public void Logout()
    {
        _currentUser = null;
        InitializeUsers();
        IsLoggedIn = false;
    }

    private async void InitializeUsers()
    {
        if (_context.Users != null)
            _users = await _context.Users.AsNoTracking().ToListAsync();
    }
    
    public User? GetCurrentUser()
    {
        return _currentUser;
    }
    
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}