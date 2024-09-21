using AutoMapper;
using LibraryServer.Application.DTO;
using LibraryServer.Domain.Common.Models;
using LibraryServer.Domain.Entities;

namespace LibraryServer.Application.Mapping;

internal class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Book, BookDTO>().ReverseMap();
        CreateMap<Author, AuthorDTO>().ReverseMap();
        CreateMap<Genre, GenreDTO>().ReverseMap();
        CreateMap<TakenBook, TakenBookDTO>().ReverseMap();
        CreateMap<PaginatedListModel<Book>, PaginatedListModel<BookDTO>>().ReverseMap();
    }
}
