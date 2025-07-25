using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;
using Services.Services;
using System.Reflection.Metadata.Ecma335;
using ViewModels;

namespace APINineTranslation.Controllers
{
    public class AdminController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IProjectService _projectService;
        private readonly IPostService _postService;

        public AdminController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserService userService,
            IConfiguration config,
            IProjectService projectService,
            IPostService postService,
            IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _userService = userService;
            _config = config;
            _projectService = projectService;
            _postService = postService;
            _mapper = mapper;
        }

        //[Authorize(Roles = "ProjectManager")]
        [HttpGet("getStaff")]
        public async Task<IActionResult> GetAllStaff()
        {
            try
            {
                var users = await _userService.GetAllStaffAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("addAccount")]
        public async Task<IActionResult> AddAccount([FromForm] CreateUserDto model)
        {
            var result = await _userService.CreateUserAsync(model);

            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors);
        }

        [HttpDelete("disableUser")]
        public async Task<IActionResult> DisableUser([FromBody] string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            user.isActive = !(user.isActive);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok($"User {(user.isActive ? "enabled" : "disabled")} successfully.");
            }
            return BadRequest(result.Errors);
        }

        [HttpGet("getAllProject")]
        public async Task<IActionResult> GetAllProjects()
        {
            try
            {
                var projects = await _projectService.GetAllProjectsAsync();
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("getAllPosts")]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            try
            {
                var posts = await _postService.GetAllPostsAsync();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
