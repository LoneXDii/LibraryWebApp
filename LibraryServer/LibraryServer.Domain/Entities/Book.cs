using LibraryServer.Domain.Entities.Abstractions;

namespace LibraryServer.Domain.Entities;

public class Book : IEntity
{
    public int Id { get; set; }
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? GenreId { get; set; }
    public Genre? Genre { get; set; }
    public int? AuthorId { get; set; }
    public Author? Author { get; set; }
    public DateTime TimeOfTake { get; set; }
    public DateTime TimeToReturn { get; set;}
    public int Quantity { get; set; }
    public string? Image { get; set; }
    //implement give to user method
}

//BookUser for many to many with TimeOfTake and TimeToReturn