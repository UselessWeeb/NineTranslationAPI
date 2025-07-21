using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using ViewModels;

namespace APINineTranslation.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public UserController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
        }

        [HttpGet("addAccount")]
        public async Task<IActionResult> AddAccount([FromBody] CreateUserViewModel model)
        {
            return Ok();
        }
    }
}
