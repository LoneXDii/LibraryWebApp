﻿using LibraryServer.DataAccess.Entities.Abstractions;

namespace LibraryServer.DataAccess.Entities;

public class Genre : Entity
{
    public string Name { get; set; } = string.Empty;
    public string NormalizedName {  get; set; } = string.Empty;
}
