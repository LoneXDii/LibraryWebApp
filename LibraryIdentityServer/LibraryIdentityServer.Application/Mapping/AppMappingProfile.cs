using AutoMapper;
using LibraryIdentityServer.Application.Models;
using LibraryIdentityServer.Domain.Common.Models;

namespace LibraryIdentityServer.Application.Mapping;

internal class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<RegisterModel, AppUser>()
            .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.EmailConfirmed,
                opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.PhoneNumber,
                opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.Name} {src.Surname}"));
    }
}
