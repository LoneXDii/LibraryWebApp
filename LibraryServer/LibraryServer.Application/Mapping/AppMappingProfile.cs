using AutoMapper;
using LibraryServer.Application.DTO;
using LibraryServer.Domain.Entities;

namespace LibraryServer.Application.Mapping;

internal class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Book, BookDTO>();
        CreateMap<Author, AuthorDTO>();
        CreateMap<Genre, GenreDTO>();
        CreateMap<BookDTO, Book>();
        CreateMap<AuthorDTO, Author>();
        CreateMap<GenreDTO, Genre>();
    }
}
