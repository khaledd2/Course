using Course.Shared;
using Course.Shared.DTOs;
using Course.Shared.Records;
using Course.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<BaseResponse<DataTableVM<CategoryDTO>>> GetAllCategoriesAsync(Pagination pagination);
        Task<BaseResponse<CategoryDTO>> GetCategoryByIdAsync(int id); 
        Task<BaseResponse<CategoryDTO>> UpdateCategoryAsync(CategoryDTO category);
        Task<BaseResponse<CategoryDTO>> CreateCategoryAsync(CategoryDTO category);
        Task<BaseResponse<CategoryDTO>> RemoveCategoryAsync(int categoryId);

    }
}
