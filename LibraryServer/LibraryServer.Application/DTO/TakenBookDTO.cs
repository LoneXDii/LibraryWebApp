using LibraryServer.Domain.Entities;

namespace LibraryServer.Application.DTO;

public class TakenBookDTO
{
    public int Id { get; set; }
    public Book Book { get; set; }
    public DateTime TimeOfTake { get; set; }
    public DateTime TimeToReturn { get; set; }
}
