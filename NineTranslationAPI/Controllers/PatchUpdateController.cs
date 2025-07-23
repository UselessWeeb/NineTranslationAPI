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

        [HttpPost("createPatchUpdate")]
        public async Task<IActionResult> CreatePatchUpdateAsync([FromBody] CreatePatchUpdateDto patchDto)
        {
            try
            {
                if (patchDto == null)
                {
                    return BadRequest("Patch update data is null.");
                }
                await _patchService.AddPatchAsync(patchDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
