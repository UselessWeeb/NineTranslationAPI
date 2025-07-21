using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Models;
using Repositories.Interfaces;
using Services.Interfaces;
using ViewModels;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(IRepository<User> userRepository, IMapper mapper, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserDto>> GetAllStaffAsync()
        {
            var users = await _userRepository.GetAllAsync();

            var userViewModels = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var viewModel = MapToViewModel(user);

                userViewModels.Add(viewModel);
            }

            return userViewModels;
        }

        private UserDto MapToViewModel(User u)
        {
            return _mapper.Map<UserDto>(u);
        }

        private IEnumerable<UserDto> MapToViewModel(IEnumerable<User> uList)
        {
            return _mapper.Map<IEnumerable<UserDto>>(uList);
        }
    }
}
