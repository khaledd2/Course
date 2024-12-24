using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Course.BLL.Interfaces;
using Course.Shared.DTOs;
using Course.Shared.Records;
using Course.Shared;

namespace Course.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseServices;

        public CourseController(ICourseService courseService)
        {
            _courseServices = courseService;
        }

        [HttpGet("GetAllCoursesAsync")]
        public async Task<IActionResult> GetAllCoursesAsync([FromQuery] Pagination pagination)
        {
            var response = await _courseServices.GetAllCoursesAsync(pagination);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("GetAllCourseNamesWithUnitNamesAsync")]
        public async Task<IActionResult> GetAllCourseNamesWithUnitNamesAsync([FromQuery] Pagination pagination)
        {
            var response = await _courseServices.GetAllCourseNamesWithUnitNamesAsync(pagination);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("GetCourseByIdAsync/{id}")]
        public async Task<IActionResult> GetCourseByIdAsync(int id)
        {
            var response = await _courseServices.GetCourseByIdAsync(id);
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }

        [HttpPost("CreateCourseAsync")]
        public async Task<IActionResult> CreateCourseAsync([FromForm] PostCourseDTO course)
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

            var response = await _courseServices.CreateCourseAsync(course);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPut("UpdateCourseAsync")]
        public async Task<IActionResult> UpdateCourseAsync([FromForm] PostCourseDTO course)
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

            var response = await _courseServices.UpdateCourseAsync(course);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete("RemoveCourseAsync/{courseId}")]
        public async Task<IActionResult> RemoveCourseAsync(int courseId)
        {
            var response = await _courseServices.RemoveCourseAsync(courseId);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
