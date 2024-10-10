using LibraryServer.Domain.Entities.Abstractions;

namespace LibraryServer.Domain.Entities;

public class Genre : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NormalizedName {  get; set; } = string.Empty;
}
