using Course.BLL.Interfaces;
using Course.BLL.Services;
using Course.Shared.DTOs;
using Course.Shared.Records;
using Course.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Course.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
            
        }

        [HttpGet("GetAllPostsAsync")]
        public async Task<IActionResult> GetAllPostsAsync([FromQuery]Pagination pagination)
        {
            var response = await _postService.GetAllPostsAsync(pagination);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("GetCourseByIdAsync/{id}")]
        public async Task<IActionResult> GetPostByIdAsync(int id)
        {
            var response = await _postService.GetPostByIdAsync(id);
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }

        [HttpPost("CreatePostAsync")]
        public async Task<IActionResult> CreatePostAsync([FromForm] PostPostDTO post)
        {
            if (!ModelState.IsValid)
            {
                // Collect error messages from ModelState
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                // Return a BaseResponse with error messages
                return BadRequest(new BaseResponse<PostPostDTO>(null, "الرجاء ادخال الحقول بطريقة صحيحة", errorMessages, false));
            }

            var response = await _postService.CreatePostAsync(post);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPut("UpdatePostAsync")]
        public async Task<IActionResult> UpdatePostAsync([FromForm] PostPostDTO post)
        {
            if (!ModelState.IsValid)
            {
                // Collect error messages from ModelState
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                // Return a BaseResponse with error messages
                return BadRequest(new BaseResponse<PostCourseDTO>(null, "الرجاء ادخال الحقول بطريقة صحيحة", errorMessages, false));
            }

            var response = await _postService.UpdatePostAsync(post);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete("RemovePostAsync/{postId}")]
        public async Task<IActionResult> RemoveCourseAsync(int postId)
        {
            var response = await _postService.RemovePostAsync(postId);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

    }
}
