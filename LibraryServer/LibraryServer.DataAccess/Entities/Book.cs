using LibraryServer.DataAccess.Entities.Abstractions;

namespace LibraryServer.Domain.Entities;

public class Book : Entity
{
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int GenreId { get; set; }
    public Genre Genre { get; set; } = new();
    public int AuthorId { get; set; }
    public Author Author { get; set; } = new();
    public DateTime TimeOfTake { get; set; }
    public DateTime TimeToReturn { get; set;}

    //implement give to user method
}
