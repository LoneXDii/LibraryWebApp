namespace LibraryServer.Application.DTO;

public class BookDTO
{
    public int Id { get; set; } = 0;
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int GenreId { get; set; }
    public string Genre { get; set; } = string.Empty;
    public int AuthorId { get; set; }
    public string Author { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string? Image { get; set; }
}
