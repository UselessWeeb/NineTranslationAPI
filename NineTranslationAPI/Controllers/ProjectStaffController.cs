using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using ViewModels;

namespace APINineTranslation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectStaffController : Controller
    {
        private readonly IProjectStaffService _projectStaffService;
        public ProjectStaffController(IProjectStaffService projectStaffService)
        {
            _projectStaffService = projectStaffService;
        }

        [Authorize(Roles = "Staff,Admin")]
        [HttpPost("addStaff")]
        public async Task<IActionResult> AddStaff([FromBody] IEnumerable<CreateProjectStaffDto> newStaff)
        {
            if (newStaff == null)
            {
                return BadRequest("New staff data is null.");
            }
            try
            {
                await _projectStaffService.AddListAsync(newStaff);
                return Ok("Staff added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[Authorize(Roles = "Staff,Admin")]
        [HttpPost("upsertStaff/{projectId}")]
        public async Task<IActionResult> SmartUpdateStaff(int projectId, [FromBody] IEnumerable<UpdateProjectStaffDto> staffUpdates)
        {
            if (staffUpdates == null || !staffUpdates.Any())
            {
                return BadRequest("Staff updates cannot be null or empty.");
            }
            try
            {
                await _projectStaffService.SmartUpdateAsync(projectId, staffUpdates);
                return Ok("Staff updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
