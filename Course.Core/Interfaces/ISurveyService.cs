using Course.Shared.DTOs;
using Course.Shared.Interfaces;
using Course.Shared.ViewModels;
using Course.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Interfaces
{
    public interface ISurveyService
    {
        Task<BaseResponse<DataTableVM<GetSurveyDTO>>> GetAllSurveysAsync(IPagination pagination);
        Task<BaseResponse<GetSurveyDTO>> GetSurveyByIdAsync(int id);
        Task<BaseResponse<PostSurveyDTO>> UpdateSurveyAsync(PostSurveyDTO survey);
        Task<BaseResponse<PostSurveyDTO>> CreateSurveyAsync(PostSurveyDTO survey);
        Task<BaseResponse<GetSurveyDTO>> RemoveSurveyAsync(int surveyId);
    }
}
