using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using ViewModels;

namespace APINineTranslation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("sortedNineListProject")]
        public async Task<IActionResult> GetSortedNineListProject()
        {
            var projects = await _projectService.GetSortedNineListAsync();
            return Ok(projects);
        }

        [HttpGet("sortedNineListPost")]
        public async Task<IActionResult> GetSortedNineListPost()
        {
            var projects = await _projectService.GetSortedNineListPostAsync();
            return Ok(projects);
        }

        [HttpGet("sortedNineListCompleted")]
        public async Task<IActionResult> GetSortedNineListCompleted()
        {
            var projects = await _projectService.GetSortedNineListCompletedAsync();
            return Ok(projects);
        }

        [HttpGet("sortedNineListOnGoing")]
        public async Task<IActionResult> GetSortedNineListOnGoing()
        {
            var projects = await _projectService.GetSortedNineListOnGoingAsync();
            return Ok(projects);
        }

        [HttpGet("sortedNineListPartner")]
        public async Task<IActionResult> GetSortedNineListPartner()
        {
            var projects = await _projectService.GetSortedNineListPartnerAsync();
            return Ok(projects);
        }

        [HttpGet("{finder}")]
        public async Task<IActionResult> GetProjectByFinder(string finder)
        {
            var project = await _projectService.GetProjectByFinderAsync(finder);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpGet("findProject/{name}")]
        public async Task<IActionResult> SearchProjectByName(string name)
        {
            try
            {
                var project = await _projectService.GetProjectByNameAsync(name);
                if (project == null)
                {
                    return NotFound();
                }
                return Ok(project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("createProject")]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto project)
        {
            if (project == null)
            {
                return BadRequest("Project data is null.");
            }
            try
            {
                await _projectService.CreateProjectAsync(project);
                return CreatedAtAction(nameof(GetProjectByFinder), new { finder = project.Finder }, project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("setCarousel/{a},{b},{c}")]
        public async Task<IActionResult> SetCarousel(int a, int b, int c)
        {
            try
            {
                await _projectService.SetCarouselAsync(a, b, c);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}