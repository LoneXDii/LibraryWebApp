using Microsoft.EntityFrameworkCore;

namespace LibraryServer.Domain.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Author>(author =>
        {
            author.ToTable("Authors");

            author.HasKey(a => a.Id);

            author.Property(a => a.Name)
                  .IsRequired()
                  .HasColumnType("longtext");

            author.Property(a => a.Surname)
                  .IsRequired()
                  .HasColumnType("longtext");

            author.Property(a => a.Country)
                  .IsRequired()
                  .HasColumnType("longtext");

            author.Property(a => a.DateOfBirth)
                  .IsRequired()
                  .HasColumnType("datetime(6)");
        });

        modelBuilder.Entity<Genre>(genre =>
        {
            genre.ToTable("Genres");

            genre.HasKey(g => g.Id);

            genre.Property(g => g.Name)
                 .IsRequired()
                 .HasColumnType("longtext");

            genre.Property(g => g.NormalizedName)
                 .IsRequired()
                 .HasColumnType("longtext");
        });

        modelBuilder.Entity<Book>(book =>
        {
            book.ToTable("Books");

            book.HasKey(b => b.Id);

            book.Property(b => b.ISBN)
                  .IsRequired()
                  .HasColumnType("longtext");

            book.Property(b => b.Title)
                  .IsRequired()
                  .HasColumnType("longtext");

            book.Property(b => b.Description)
                  .IsRequired()
                  .HasColumnType("longtext");

            book.Property(b => b.Quantity)
                  .IsRequired()
                  .HasColumnType("int");

            book.Property(b => b.Image)
                  .HasColumnType("longtext");

            book.HasOne(b => b.Author)
                  .WithMany(a => a.Books)
                  .HasForeignKey(b => b.AuthorId);

            book.HasOne(b => b.Genre)
                  .WithMany()
                  .HasForeignKey(b => b.GenreId);
        });

        modelBuilder.Entity<TakenBook>(takenBook =>
        {
            takenBook.ToTable("TakenBooks");

            takenBook.HasKey(tb => tb.Id);

            takenBook.Property(tb => tb.UserId)
                     .HasColumnType("longtext");

            takenBook.Property(tb => tb.TimeOfTake)
                     .IsRequired()
                     .HasColumnType("datetime(6)");

            takenBook.Property(tb => tb.TimeToReturn)
                     .IsRequired()
                     .HasColumnType("datetime(6)");

            takenBook.HasOne(tb => tb.Book)
                     .WithMany()
                     .HasForeignKey(tb => tb.BookId);
        });
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<TakenBook> TakenBooks { get; set; }
}
