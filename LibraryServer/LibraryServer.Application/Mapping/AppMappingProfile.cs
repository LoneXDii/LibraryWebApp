using AutoMapper;
using LibraryServer.Application.DTO;

namespace LibraryServer.Application.Mapping;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Book, BookDTO>().ForMember(dest => dest.Author, 
                                             opt => opt.MapFrom(src => src.Author == null ? "" : $"{src.Author.Name} {src.Author.Surname}"))
                                  .ForMember(dest => dest.Genre,
                                             opt => opt.MapFrom(src => src.Genre == null ? "" : src.Genre.Name));
        CreateMap<BookDTO, Book>().ForMember(dest => dest.Author, opt => opt.Ignore())
                                  .ForMember(dest => dest.Genre, opt => opt.Ignore());
        CreateMap<Author, AuthorDTO>().ReverseMap();
        CreateMap<Genre, GenreDTO>().ReverseMap();
        CreateMap<TakenBook, TakenBookDTO>().ForMember(dest => dest.Book,
                                             opt => opt.MapFrom(src => src.Book))
                                            .ReverseMap();
        CreateMap<PaginatedListModel<Book>, PaginatedListModel<BookDTO>>().ReverseMap();
    }
}
