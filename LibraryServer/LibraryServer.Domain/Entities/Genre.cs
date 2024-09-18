using LibraryServer.Domain.Entities.Abstractions;

namespace LibraryServer.Domain.Entities;

public class Genre : Entity
{
    public string Name { get; set; } = string.Empty;
    public string NormalizedName {  get; set; } = string.Empty;
}
