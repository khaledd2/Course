using Course.Shared;
using Course.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Interfaces
{
    public interface ICategoryService
    {
        public Task<BaseResponse<IEnumerable<CategoryDTO>>> GetAllCategoriesAsync();
        public Task<BaseResponse<CategoryDTO>> GetCategoryByIdAsync(int id); 
        public Task<BaseResponse<CategoryDTO>> UpdateCategoryAsync(CategoryDTO category);
        public Task<BaseResponse<CategoryDTO>> CreateCategoryAsync(CategoryDTO category);
        public Task<BaseResponse<CategoryDTO>> RemoveCategoryAsync(int categoryId);

    }
}
