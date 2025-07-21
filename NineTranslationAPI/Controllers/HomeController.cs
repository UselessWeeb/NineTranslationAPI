using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace APINineTranslation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public HomeController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("carousel/{x},{y},{z}")]
        public async Task<IActionResult> GetCarouselProjects(int x, int y, int z)
        {
            var projects = await _projectService.GetCarouselProjectsAsync(x, y, z);
            return Ok(projects);
        }

        [HttpGet("sorted-nine-list")]
        public async Task<IActionResult> GetSortedNineList()
        {
            var projects = await _projectService.GetSortedNineListAsync();
            return Ok(projects);
        }

        [HttpGet("random-three")]
        public async Task<IActionResult> GetRandomThreeProjects()
        {
            var projects = await _projectService.GetRandomThreeProjectsAsync();
            return Ok(projects);
        }
    }
}