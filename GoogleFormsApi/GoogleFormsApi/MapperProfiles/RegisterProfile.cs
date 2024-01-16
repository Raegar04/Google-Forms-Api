using AutoMapper;
using Domain.Models;

public class RegisterProfile : Profile
{
    /// <summary>
    /// Specified configuration for mapper
    /// </summary>
    public RegisterProfile()
    {
        CreateMap<RegistrationRequest, AppUser>();
    }
}
