using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using ViewModels;

namespace APINineTranslation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatchUpdateController : Controller
    {
        private readonly IPatchService _patchService;

        public PatchUpdateController(IPatchService patchService)
        {
            _patchService = patchService;
        }

        [Authorize(Roles = "Staff,Admin")]
        [HttpPost("createPatchUpdate")]
        public async Task<IActionResult> CreatePatchUpdateAsync([FromBody] IEnumerable<CreatePatchUpdateDto> patchDto)
        {
            try
            {
                if (patchDto == null)
                {
                    return BadRequest("Patch update data is null.");
                }
                await _patchService.AddPatchListAsync(patchDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Staff,Admin")]
        [HttpPost("upsertPatchUpdate/{projectId}")]
        public async Task<IActionResult> SmartUpdatePatchAsync(int projectId, [FromBody] IEnumerable<UpdatePatchUpdateDto> patchUpdates)
        {
            try
            {
                if (patchUpdates == null || !patchUpdates.Any())
                {
                    return BadRequest("Patch updates cannot be null or empty.");
                }
                await _patchService.SmartUpdatePatchAsync(projectId, patchUpdates);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
