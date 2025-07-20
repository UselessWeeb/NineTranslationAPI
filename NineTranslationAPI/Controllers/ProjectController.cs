using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace APINineTranslation.Controllers
{
    [Route("project")]
    [ApiController]
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
    }
}