using Course.Shared.DTOs;
using Course.Shared.Records;
using Course.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Interfaces
{
    public interface ILessonService
    {
        Task<BaseResponse<GetOneLessonDTO>> GetLessonByIdAsync(int id);
        Task<BaseResponse<PostLessonDTO>> UpdateLessonAsync(PostLessonDTO lesson);
        Task<BaseResponse<PostLessonDTO>> CreateLessonAsync(PostLessonDTO lesson);
        Task<BaseResponse<GetOneLessonDTO>> RemoveLessonAsync(int lessonId);
    }
}
