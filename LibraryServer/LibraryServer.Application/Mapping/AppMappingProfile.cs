using AutoMapper;
using LibraryServer.Application.DTO;
using LibraryServer.Domain.Common.Models;
using LibraryServer.Domain.Entities;

namespace LibraryServer.Application.Mapping;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Book, BookDTO>().ForMember(dest => dest.Author, 
                                             opt => opt.MapFrom(src => src.Author == null ? "" : $"{src.Author.Name} {src.Author.Surname}"))
                                  .ReverseMap();
        CreateMap<Author, AuthorDTO>().ReverseMap();
        CreateMap<Genre, GenreDTO>().ReverseMap();
        CreateMap<TakenBook, TakenBookDTO>().ReverseMap();
        CreateMap<PaginatedListModel<Book>, PaginatedListModel<BookDTO>>().ReverseMap();
    }
}
