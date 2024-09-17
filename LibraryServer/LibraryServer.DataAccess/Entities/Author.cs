using LibraryServer.DataAccess.Entities.Abstractions;

namespace LibraryServer.Domain.Entities;

internal class Author : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
}
