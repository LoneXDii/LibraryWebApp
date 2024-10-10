using LibraryServer.DataAccess.Entities.Abstractions;

namespace LibraryServer.DataAccess.Entities;

public class TakenBook : IEntity
{
    public int Id { get; set; }
    public int? BookId { get; set; }
    public Book? Book { get; set; }
    public string? UserId { get; set; }
    public DateTime TimeOfTake { get; set; }
    public DateTime TimeToReturn { get; set; }
}
