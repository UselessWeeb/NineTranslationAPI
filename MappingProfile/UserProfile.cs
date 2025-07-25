using AutoMapper;
using Models;
using ViewModels;

namespace MappingProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ForMember(e => e.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<CreateUserDto, User>();
        }
    }
}