﻿using LibraryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Data;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext()
    {
        Database.Migrate();
    }

    public DbSet<User>? Users { get; set; }
    public DbSet<Book>? Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=LibraryDb.db");
    }
}