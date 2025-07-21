using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Reflection.Metadata.Ecma335;
using ViewModels;

namespace APINineTranslation.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;

        public UserController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
        }

        [HttpPost("addAccount")]
        public async Task<IActionResult> AddAccount([FromBody] CreateUserDto model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                DisplayName = model.DisplayName ?? model.UserName ?? model.Email,
                JoinDate = model.JoinDate,
                ProfilePicture = model.ProfilePictureUrl ?? "default-profile.png"
            };

            if (!string.IsNullOrWhiteSpace(model.Role))
            {
                if (!await _roleManager.RoleExistsAsync(model.Role))
                {
                    return BadRequest();
                }
                else
                {
                    var result = await _userManager.CreateAsync(user, model.Password!);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, model.Role);

                        return Ok();
                    }

                    return BadRequest();
                }
            }
            return BadRequest();
        }
    }
}
