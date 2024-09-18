using LibraryServer.DataAccess.Data;
using LibraryServer.Domain.Entities;

namespace LibraryServer.API.Temp;

public class DbInitializer
{
    public static async Task SeedData(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        List<Genre> genres = new List<Genre>
        {
            new Genre 
            { 
                Name = "Science Fiction",                     
                NormalizedName = "science-fiction"
            },
            new Genre
            { 
                Name = "Fantasy",
                NormalizedName = "fantasy"
            },
            new Genre 
            { 
                Name = "Mystery",
                NormalizedName = "mystery"
            },
            new Genre 
            { 
                Name = "Thriller", 
                NormalizedName = "thriller"
            },
            new Genre 
            { 
                Name = "Romance",
                NormalizedName = "romance"
            },
            new Genre 
            { 
                Name = "Horror",
                NormalizedName = "horror"
            },
            new Genre 
            { 
                Name = "Historical",
                NormalizedName = "historical"
            },
            new Genre 
            { 
                Name = "Biography",
                NormalizedName = "biography"
            },
            new Genre 
            { 
                Name = "Self-Help",
                NormalizedName = "self-help"
            },
            new Genre 
            { 
                Name = "Non-Fiction",
                NormalizedName = "non-fiction"
            }
        };

        List<Author> authors = new List<Author>
        {
            new Author
            {
                Name = "George",
                Surname = "Orwell",
                Country = "United Kingdom",
                DateOfBirth = new DateTime(1903, 6, 25)
            },
            new Author
            {
                Name = "Jane",
                Surname = "Austen",
                Country = "United Kingdom",
                DateOfBirth = new DateTime(1775, 12, 16)
            },
            new Author
            {
                Name = "Mark",
                Surname = "Twain",
                Country = "United States",
                DateOfBirth = new DateTime(1835, 11, 30)
            },
            new Author
            {
                Name = "Haruki",
                Surname = "Murakami",
                Country = "Japan",
                DateOfBirth = new DateTime(1949, 1, 12)
            },
            new Author
            {
                Name = "Chinua",
                Surname = "Achebe",
                Country = "Nigeria",
                DateOfBirth = new DateTime(1930, 11, 16)
            },
            new Author
            {
                Name = "Gabriel",
                Surname = "García Márquez",
                Country = "Colombia",
                DateOfBirth = new DateTime(1927, 3, 6)
            },
            new Author
            {
                Name = "J.K.",
                Surname = "Rowling",
                Country = "United Kingdom",
                DateOfBirth = new DateTime(1965, 7, 31)
            },
            new Author
            {
                Name = "Ernest",
                Surname = "Hemingway",
                Country = "United States",
                DateOfBirth = new DateTime(1899, 7, 21)
            },
            new Author
            {
                Name = "Fyodor",
                Surname = "Dostoevsky",
                Country = "Russia",
                DateOfBirth = new DateTime(1821, 11, 11)
            },
            new Author
            {
                Name = "Isabel",
                Surname = "Allende",
                Country = "Chile",
                DateOfBirth = new DateTime(1942, 8, 2)
            }
        };

        List<Book> books = new List<Book>
        {
            new Book
            {
                ISBN = "978-1-56619-909-4",
                Title = "1984",
                Description = "Dystopian novel",
                GenreId = 1,
                AuthorId = 1,
                Quantity = 5,
                Image = null
            },
                new Book
                {
                    ISBN = "978-1-56619-910-0",
                    Title = "Pride and Prejudice",
                    Description = "Romantic novel",
                    GenreId = 5,
                    AuthorId = 2,
                    Quantity = 3,
                    Image = null
                },
                new Book
                {
                    ISBN = "978-1-56619-911-7",
                    Title = "Adventures of Huckleberry Finn",
                    Description = "Adventure novel",
                    GenreId = 3,
                    AuthorId = 3,
                    Quantity = 4,
                    Image = null
                },
                new Book
                {
                    ISBN = "978-1-56619-912-4",
                    Title = "Norwegian Wood",
                    Description = "Romantic drama",
                    GenreId = 5,
                    AuthorId = 4,
                    Quantity = 6,
                    Image = null
                },
                new Book
                {
                    ISBN = "978-1-56619-913-1",
                    Title = "Things Fall Apart",
                    Description = "Historical novel",
                    GenreId = 7,
                    AuthorId = 5,
                    Quantity = 7,
                    Image = null
                },
                new Book
                {
                    ISBN = "978-1-56619-914-8",
                    Title = "One Hundred Years of Solitude",
                    Description = "Magic realism",
                    GenreId = 7,
                    AuthorId = 6,
                    Quantity = 2,
                    Image = null
                },
                new Book
                {
                    ISBN = "978-1-56619-915-5",
                    Title = "Harry Potter and the Philosopher's Stone",
                    Description = "Fantasy novel",
                    GenreId = 2,
                    AuthorId = 7,
                    Quantity = 10,
                    Image = null
                },
                new Book
                {
                    ISBN = "978-1-56619-916-2",
                    Title = "The Old Man and the Sea",
                    Description = "Short novel",
                    GenreId = 3,
                    AuthorId = 8,
                    Quantity = 8,
                    Image = null
                },
                new Book
                {
                    ISBN = "978-1-56619-917-9",
                    Title = "Crime and Punishment",
                    Description = "Psychological novel",
                    GenreId = 4,
                    AuthorId = 9,
                    Quantity = 5,
                    Image = null
                },
                new Book
                {
                    ISBN = "978-1-56619-918-6",
                    Title = "The House of the Spirits",
                    Description = "Magical realism",
                    GenreId = 7,
                    AuthorId = 10,
                    Quantity = 4,
                    Image = null
                },
                new Book
                {
                    ISBN = "978-1-56619-919-3",
                    Title = "Sample Book 1",
                    Description = "Sample description",
                    GenreId = 1,
                    AuthorId = 2,
                    Quantity = 5,
                    Image = null
                },
                new Book
                {
                    ISBN = "978-1-56619-920-9",
                    Title = "Sample Book 2",
                    Description = "Sample description",
                    GenreId = 2,
                    AuthorId = 3,
                    Quantity = 5,
                    Image = null
                },
                new Book
                {
                    ISBN = "978-1-56619-921-6",
                    Title = "Sample Book 3",
                    Description = "Sample description",
                    GenreId = 3,
                    AuthorId = 4,
                    Quantity = 5,
                    Image = null
                },
                new Book
                {
                    ISBN = "978-1-56619-922-3",
                    Title = "Sample Book 4",
                    Description = "Sample description",
                    GenreId = 4,
                    AuthorId = 5,
                    Quantity = 5,
                    Image = null
                },
                new Book
                {
                    ISBN = "978-1-56619-923-0",
                    Title = "Sample Book 5",
                    Description = "Sample description",
                    GenreId = 5,
                    AuthorId = 6,
                    Quantity = 5,
                    Image = null
                },
                new Book
                {
                    ISBN = "978-1-56619-924-7",
                    Title = "Sample Book 6",
                    Description = "Sample description",
                    GenreId = 6,
                    AuthorId = 7,
                    Quantity = 5,
                    Image = null
                },
                new Book
                {
                    ISBN = "978-1-56619-925-4",
                    Title = "Sample Book 7",
                    Description = "Sample description",
                    GenreId = 7,
                    AuthorId = 8,
                    Quantity = 5,
                    Image = null
                },
                new Book
                {
                    ISBN = "978-1-56619-926-1",
                    Title = "Sample Book 8",
                    Description = "Sample description",
                    GenreId = 8,
                    AuthorId = 9,
                    Quantity = 5,
                    Image = null
                },
                new Book
                {
                    ISBN = "978-1-56619-927-8",
                    Title = "Sample Book 9",
                    Description = "Sample description",
                    GenreId = 9,
                    AuthorId = 10,
                    Quantity = 5,
                    Image = null
                }
        };


        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
        await dbContext.Genres.AddRangeAsync(genres);
        await dbContext.Authors.AddRangeAsync(authors);
        await dbContext.SaveChangesAsync();
        await dbContext.Books.AddRangeAsync(books);
        await dbContext.SaveChangesAsync();
    }
}
