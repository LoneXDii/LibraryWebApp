using LibraryServer.DataAccess.Entities.Abstractions;

namespace LibraryServer.Domain.Entities;

internal class Genre : Entity
{
    public string Name { get; set; } = string.Empty;
}
