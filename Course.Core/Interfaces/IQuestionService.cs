using Course.Shared.DTOs;
using Course.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Interfaces
{
    public interface IQuestionService
    {
        Task<BaseResponse<List<GetQuestionDTO>>> GetAllQuestionsBySurveyIdAsync(int surveyId);
        Task<BaseResponse<PostQuestionDTO>> CreateQuestionAsync(PostQuestionDTO question);
        Task<BaseResponse<PostQuestionDTO>> UpdateQuestionAsync(PostQuestionDTO question);
        Task<BaseResponse<PostQuestionDTO>> RemoveQuestionAsync(int questionId);
    }
}
