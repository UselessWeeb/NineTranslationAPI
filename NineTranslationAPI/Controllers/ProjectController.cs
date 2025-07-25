using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System.Data.Common;
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

        [Authorize(Roles = "Admin")]
        [HttpGet("setCarousel/{a}")]
        public async Task<IActionResult> SetCarousel(int a)
        {
            try
            {
                await _projectService.SetCarouselAsync(a);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Staff,Admin")]
        [HttpPost("createProject")]
        public async Task<IActionResult> CreateProject([FromForm] CreateProjectDto project)
        {
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(60);
            if (project == null)
            {
                return BadRequest("Project data is null.");
            }
            try
            {
                int detailId = await _projectService.CreateProjectAsync(project);
                return Ok(detailId);
            }
            catch (DbUpdateException dbEx)
            {
                return BadRequest($"Duplication finder.{dbEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Staff,Admin")]
        [HttpPost("update")]
        public async Task<IActionResult> UpdateProject([FromForm] UpdateProjectDto project)
        {
            if (project == null)
            {
                return BadRequest("Project data is null.");
            }
            try
            {
                int detailId = await _projectService.UpdateProjectAsync(project);
                return Ok(detailId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[HttpDelete("deleteProject/{finder}")]
        //public async Task<IActionResult> DeleteProjectByFinder(string finder)
        //{
        //    try
        //    {
        //        await _projectService.DeleteProjectByFinderAsync(finder);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        //[HttpDelete("deleteProjectById/{id}")]
        //public async Task<IActionResult> DeleteProjectById(int id)
        //{
        //    try
        //    {
        //        await _projectService.DeleteProjectById(id);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        [Authorize(Roles = "Admin")]
        [HttpDelete("disableProject/{finder}")]
        public async Task<IActionResult> DisableProject(string finder)
        {
            try
            {
                await _projectService.DisableProjectAsync(finder);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}