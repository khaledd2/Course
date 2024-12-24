using Course.Shared.DTOs;
using Course.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course.Shared.Records;
using Course.Shared.ViewModels;
using Course.Shared.Interfaces;

namespace Course.BLL.Interfaces
{
    public interface ICourseService
    {
        Task<BaseResponse<DataTableVM<GetOneCourseDTO>>> GetAllCoursesAsync(IPagination pagination);
        Task<BaseResponse<GetOneCourseDTO>> GetCourseByIdAsync(int id);
        Task<BaseResponse<DataTableVM<GetAllCourseNamesWithUnitNamesDTO>>> GetAllCourseNamesWithUnitNamesAsync(IPagination pagination);
        Task<BaseResponse<PostCourseDTO>> UpdateCourseAsync(PostCourseDTO course);
        Task<BaseResponse<PostCourseDTO>> CreateCourseAsync(PostCourseDTO course);
        Task<BaseResponse<GetOneCourseDTO>> RemoveCourseAsync(int course);
    }
}
