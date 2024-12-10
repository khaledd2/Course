using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Course.BLL.Interfaces;
using Course.Shared.DTOs;
namespace Course.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryServices;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryServices = categoryService;
        }
        
        [HttpGet("GetAllCategoriesAsync")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var response = await _categoryServices.GetAllCategoriesAsync();
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("GetCategoryByIdAsync/{id}")]
        public async Task<IActionResult> GetCategoryByIdAsync(int id)
        {
            var response = await _categoryServices.GetCategoryByIdAsync(id);
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }

        [HttpPost("CreateCategoryAsync")]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryDTO category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _categoryServices.CreateCategoryAsync(category);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPut("UpdateCategoryAsync")]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] CategoryDTO category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _categoryServices.UpdateCategoryAsync(category);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete("RemoveCategoryAsync")]
        public async Task<IActionResult> RemoveCategoryAsync(int categoryId)
        {
            var response = await _categoryServices.RemoveCategoryAsync(categoryId);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
