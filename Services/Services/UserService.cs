﻿using AutoMapper;
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
            {
                user.ProfilePicture = "https://example.com/default-profile.png";
            }

            var result = await _userManager.CreateAsync(user, model.Password!);
            if (!result.Succeeded)
                return result;

            var roleResult = await _userManager.AddToRoleAsync(user, "Staff");
            return roleResult;
        }

        public async Task<IEnumerable<UserDto>> GetAllStaffAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var staffUsers = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var viewModel = MapToViewModel(user);
                viewModel.Roles = roles;
                staffUsers.Add(viewModel);
            }

            return staffUsers;
        }

        public Task<UserDto> GetStaffByIdAsync(string id)
        {
            var user = _userRepository.FindAsync(u => u.Id == id).Result.FirstOrDefault();
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            var roles = _userManager.GetRolesAsync(user).Result;
            var viewModel = MapToViewModel(user);
            viewModel.Roles = roles;
            return Task.FromResult(viewModel);
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
