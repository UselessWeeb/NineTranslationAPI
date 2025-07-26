using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;
using Services.Interfaces;
using Services.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ViewModels;

namespace APINineTranslation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IPasswordTokenService _passwordTokenService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;

        public LoginController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IPasswordTokenService passwordTokenService,
            IEmailService emailService,
            IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _passwordTokenService = passwordTokenService;
            _emailService = emailService;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null) user = await _userManager.FindByEmailAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                // Add each role as a separate claim
                foreach (var role in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Issuer"],
                    expires: DateTime.UtcNow.AddHours(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return Unauthorized();
        }

        [Authorize(Roles = "Staff,Admin")]
        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePasswordById([FromBody] ChangePasswordDto model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await _passwordTokenService.CreatePasswordTokenAsync(user.Id, model.NewPassword);
            if (result != null)
            {
                string subject = "Password Reset";
                string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "PasswordReset.html");
                string body = await System.IO.File.ReadAllTextAsync(templatePath);

                body = body.Replace("{{FullName}}", user.DisplayName)
                           .Replace("{{Token}}", result);

                await _emailService.SendEmailAsync(user.Email, subject, body);
                return Ok("Password change token created successfully.");
            }
            return BadRequest();
        }

        [HttpPost("authenticateToken")]
        public async Task<IActionResult> AuthenticateToken([FromBody] AuthenticateTokenDto model)
        {
            var isValid = await _passwordTokenService.IsTokenValidAsync(model.Token);
            if (!isValid)
                return Unauthorized("Invalid or expired token.");

            var userId = await _passwordTokenService.GetUserIdFromTokenAsync(model.Token);
            if (userId == null)
                return NotFound("User not found.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("User not found.");

            var removeResult = await _userManager.RemovePasswordAsync(user);
            if (!removeResult.Succeeded)
                return BadRequest(removeResult.Errors);

            var addResult = await _userManager.AddPasswordAsync(user, model.Token);
            if (!addResult.Succeeded)
                return BadRequest(addResult.Errors);

            await _passwordTokenService.MarkTokenAsUsedAsync(model.Token);
            return Ok("Password changed successfully.");
        }

        [Authorize(Roles = "Staff,Admin")]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _signInManager.SignOutAsync();
            return Ok("Logged out successfully.");
        }
    }
}