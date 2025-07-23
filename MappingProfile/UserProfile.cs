using AutoMapper;
using Models;
using ViewModels;

namespace MappingProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<CreateUserDto, User>();
        }
    }
}