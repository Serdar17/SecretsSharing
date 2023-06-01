using AutoMapper;
using Domain.Models;
using WebApi.Dtos.Auth;

namespace WebApi.Mappings;

public class ApplicationUserProfile : Profile
{
    public ApplicationUserProfile()
    {
        CreateMap<AuthorizationModel, ApplicationUser>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
            .ReverseMap();
    }
}