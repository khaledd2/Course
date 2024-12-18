using Course.BLL.Interfaces;
using Course.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {

        private readonly IUnitService _unitService;
        public UnitsController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [HttpPost("CreateUnitAsync")]
        public async Task<IActionResult> CreateUnitAsync([FromBody] UnitDTO unit)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _unitService.CreateUnitAsync(unit);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPut("UpdateUnitAsync")]
        public async Task<IActionResult> UpdateUnitAsync([FromBody] UnitDTO unit)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _unitService.UpdateUnitAsync(unit);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete("RemoveUnitAsync")]
        public async Task<IActionResult> RemoveUnitAsync(int unitId)
        {
            var response = await _unitService.RemoveUnitAsync(unitId);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

    }
}
