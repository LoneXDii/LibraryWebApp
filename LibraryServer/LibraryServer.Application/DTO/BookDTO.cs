using LibraryServer.Domain.Entities;

namespace LibraryServer.Application.DTO;

public class BookDTO
{
    public int Id { get; set; }
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int GenreId { get; set; }
    public int AuthorId { get; set; }
    public int Quantity { get; set; }
    public string? Image { get; set; }
}
