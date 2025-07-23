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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IImageService _imageService;

        public UserService(IRepository<User> userRepository, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IImageService imageService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _imageService = imageService;
        }

        public async Task<IdentityResult> CreateUserAsync(CreateUserDto model)
        {
            var user = _mapper.Map<User>(model);

            if (!string.IsNullOrWhiteSpace(model.Role.ToString()))
            {
                if (model.ProfilePictureFile != null)
                {
                    var profileImageUrl = await _imageService.UploadImageAsync(model.ProfilePictureFile);
                    user.ProfilePicture = profileImageUrl != null ? profileImageUrl.Url.ToString() : "https://example.com/default-profile.png";
                }
                else
                {
                    user.ProfilePicture = "https://example.com/default-profile.png";
                }

                if (!await _roleManager.RoleExistsAsync(model.Role.ToString()))
                {
                    return IdentityResult.Failed(new IdentityError { Description = "Invalid role" });
                }

                var result = await _userManager.CreateAsync(user, model.Password!);
                if (!result.Succeeded)
                    return result;

                var roleResult = await _userManager.AddToRoleAsync(user, model.Role.ToString());
                return roleResult;
            }

            return IdentityResult.Failed(new IdentityError { Description = "Role is required" });
        }

        public async Task<IEnumerable<UserDto>> GetAllStaffAsync()
        {
            var users = await _userRepository.FindAsync(u => u.isActive);

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
