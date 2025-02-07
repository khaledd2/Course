using Course.BLL.Interfaces;
using Course.BLL.Services;
using Course.Shared.DTOs;
using Course.Shared.Records;
using Course.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        public SurveysController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        [HttpGet("GetAllSurveysAsync")]
        public async Task<IActionResult> GetAllSurveysAsync([FromQuery] Pagination pagination)
        {
            var response = await _surveyService.GetAllSurveysAsync(pagination);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("GetSurveyByIdAsync/{id}")]
        public async Task<IActionResult> GetSurveyByIdAsync(int id)
        {
            var response = await _surveyService.GetSurveyByIdAsync(id);
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }

        [HttpPost("CreateSurveyAsync")]
        public async Task<IActionResult> CreateSurveyAsync(/*[FromForm]*/ PostSurveyDTO survey)
        {
            var response = await _surveyService.CreateSurveyAsync(survey);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPut("UpdateSurveyAsync")]
        public async Task<IActionResult> UpdateCourseAsync(/*[FromForm]*/ PostSurveyDTO survey)
        {
            var response = await _surveyService.UpdateSurveyAsync(survey);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete("RemoveSurveyAsync/{surveyId}")]
        public async Task<IActionResult> RemoveSurveyAsync(int surveyId)
        {
            var response = await _surveyService.RemoveSurveyAsync(surveyId);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

    }
}
