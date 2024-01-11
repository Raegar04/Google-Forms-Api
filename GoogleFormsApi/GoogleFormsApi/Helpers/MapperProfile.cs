using AutoMapper;
using Core.Models;

public class MapperProfile:Profile
    {
        /// <summary>
        /// Specified configuration for mapper
        /// </summary>
        public MapperProfile()
        {
            CreateMap<RegistrationRequest, AppUser>();
        }
    }
