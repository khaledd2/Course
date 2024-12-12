using Course.Shared.DTOs;
using Course.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course.Shared.Records;

namespace Course.BLL.Interfaces
{
    public interface ICourseService
    {
        public Task<BaseResponse<IEnumerable<GetAllCoursesDTO>>> GetAllCoursesAsync(Pagination pagination);
        public Task<BaseResponse<GetOneCourseDTO>> GetCourseByIdAsync(int id);
        public Task<BaseResponse<PostCourseDTO>> UpdateCourseAsync(PostCourseDTO course);
        public Task<BaseResponse<PostCourseDTO>> CreateCourseAsync(PostCourseDTO course);
        public Task<BaseResponse<GetOneCourseDTO>> RemoveCourseAsync(int course);
    }
}
