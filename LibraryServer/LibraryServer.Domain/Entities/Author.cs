﻿using LibraryServer.Domain.Entities.Abstractions;

namespace LibraryServer.Domain.Entities;

public class Author : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public List<Book>? Books { get; set; }
    public DateTime DateOfBirth { get; set; }
}
