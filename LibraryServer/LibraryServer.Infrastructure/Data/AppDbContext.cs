using Microsoft.EntityFrameworkCore;

namespace LibraryServer.Domain.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("Authors");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasColumnType("longtext");

            entity.Property(e => e.Surname)
                  .IsRequired()
                  .HasColumnType("longtext");

            entity.Property(e => e.Country)
                  .IsRequired()
                  .HasColumnType("longtext");

            entity.Property(e => e.DateOfBirth)
                  .IsRequired()
                  .HasColumnType("datetime(6)");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genres");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasColumnType("longtext");

            entity.Property(e => e.NormalizedName)
                  .IsRequired()
                  .HasColumnType("longtext");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Books");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.ISBN)
                  .IsRequired()
                  .HasColumnType("longtext");

            entity.Property(e => e.Title)
                  .IsRequired()
                  .HasColumnType("longtext");

            entity.Property(e => e.Description)
                  .IsRequired()
                  .HasColumnType("longtext");

            entity.Property(e => e.Quantity)
                  .IsRequired()
                  .HasColumnType("int");

            entity.Property(e => e.Image)
                  .HasColumnType("longtext");

            entity.HasOne(d => d.Author)
                  .WithMany(p => p.Books)
                  .HasForeignKey(d => d.AuthorId);

            entity.HasOne(d => d.Genre)
                  .WithMany()
                  .HasForeignKey(d => d.GenreId);
        });

        modelBuilder.Entity<TakenBook>(entity =>
        {
            entity.ToTable("TakenBooks");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.UserId)
                  .HasColumnType("longtext");

            entity.Property(e => e.TimeOfTake)
                  .IsRequired()
                  .HasColumnType("datetime(6)");

            entity.Property(e => e.TimeToReturn)
                  .IsRequired()
                  .HasColumnType("datetime(6)");

            entity.HasOne(d => d.Book)
                  .WithMany()
                  .HasForeignKey(d => d.BookId);
        });
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<TakenBook> TakenBooks { get; set; }
}
