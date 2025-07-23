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

        [HttpPost("createPost")]
        public async Task<IActionResult> CreatePostAsync([FromForm] CreatePostDto postDto)
        {
            try
            {
                if (postDto == null)
                {
                    return BadRequest("Post data is null.");
                }
                await _postService.CreatePostAsync(postDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //[HttpDelete("deletePost/{finder}")]
        //public async Task<IActionResult> DeletePostAsync(string finder)
        //{
        //    try
        //    {
        //        await _postService.DeletePostASync(finder);
        //        return Ok($"Post with finder '{finder}' deleted successfully.");
        //    }
        //    catch (KeyNotFoundException knfEx)
        //    {
        //        return NotFound(knfEx.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpDelete("deletePostById/{id}")]
        //public async Task<IActionResult> DeletePostByIdAsync(int id)
        //{
        //    try
        //    {
        //        await _postService.DeletePostAsync(id);
        //        return Ok($"Post with ID '{id}' deleted successfully.");
        //    }
        //    catch (KeyNotFoundException knfEx)
        //    {
        //        return NotFound(knfEx.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPost("disablePost/{finder}")]
        public async Task<IActionResult> DisablePostAsync(string finder)
        {
            try
            {
                await _postService.DisablePost(finder);
                return Ok($"Post with finder '{finder}' disabled successfully.");
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
