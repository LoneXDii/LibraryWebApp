using AutoMapper;
using LibraryServer.Application.DTO;
using LibraryServer.Domain.Entities;

namespace LibraryServer.Application.Mapping;

internal class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Book, BookDTO>().ReverseMap();
        CreateMap<Author, AuthorDTO>().ReverseMap();
        CreateMap<Genre, GenreDTO>().ReverseMap();
    }
}
