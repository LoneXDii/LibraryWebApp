using LibraryServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryServer.DataAccess.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    DbSet<Book> Books { get; set; }
    DbSet<Author> Authors { get; set; }
    DbSet<Genre> Genres { get; set; }
}
