using Course.BLL.Interfaces;
using Course.BLL.Services;
using Course.Shared.DTOs;
using Course.Shared.Records;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        public LessonsController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpPost("CreateLessonAsync")]
        public async Task<IActionResult> CreateLessonAsync(PostLessonDTO lesson)
        {
            var response = await _lessonService.CreateLessonAsync(lesson);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        
        [HttpGet("GetLessonByIdAsync")]
        public async Task<IActionResult> GetLessonByIdAsync(int id)
        {
            var response = await _lessonService.GetLessonByIdAsync(id);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        
        [HttpPut("UpdateLessonAsync")]
        public async Task<IActionResult> UpdateLessonAsync(PostLessonDTO lesson)
        {
            var response = await _lessonService.UpdateLessonAsync(lesson);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete("RemoveLessonAsync")]
        public async Task<IActionResult> RemoveLessonAsync(int id)
        {
            var response = await _lessonService.RemoveLessonAsync(id);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }


    }
}
