using Course.Shared;
using Course.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Interfaces
{
    public interface IAnswerService
    {
        Task<BaseResponse<List<AnswerDTO>>> GetAllAnswersByQuestionIdAsync(int questionId);
    }
}
