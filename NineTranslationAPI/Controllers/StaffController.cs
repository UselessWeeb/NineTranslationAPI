using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace APINineTranslation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : Controller
    {
        private readonly IUserService _userService;

        public StaffController(IUserService userService)
        {
            _userService = userService;
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
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
