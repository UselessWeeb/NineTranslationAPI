using Dto;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using ViewModels;

namespace APINineTranslation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("findPost/{name}")]
        public async Task<IActionResult> GetPostByNameAsync(string name)
        {
            var post = await _postService.getPostByNameAsync(name);
            if (post == null)
            {
                return NotFound($"Post with name '{name}' not found.");
            }
            return Ok(post);
        }
    }
}
