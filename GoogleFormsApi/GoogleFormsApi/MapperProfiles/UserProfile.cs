using Application.Responses;
using AutoMapper;
using Domain.Models;
using GoogleFormsApi.Responses;

public class UserProfile : Profile
{
    /// <summary>
    /// Specified configuration for mapper
    /// </summary>
    public UserProfile()
    {
        CreateMap<RegistrationRequest, AppUser>();
        CreateMap<AppUser, UserResponse>().
            ForMember(mem=> mem.Password, opt=>opt.MapFrom(src=>src.PasswordHash));
        CreateMap<AppUser, UserDetailsResponse>();
    }
}
