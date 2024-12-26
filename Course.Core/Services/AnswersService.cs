using AutoMapper;
using Course.BLL.Interfaces;
using Course.DAL;
using Course.DAL.Models;
using Course.Shared;
using Course.Shared.Constants;
using Course.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Services
{
    public class AnswersService : IAnswerService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public AnswersService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        
        public async Task<BaseResponse<List<AnswerDTO>>> GetAllAnswersByQuestionIdAsync(int questionId)
        {
            try
            {
                var items = await _db.Answers.Where(q => q.QuestionId == questionId).ToListAsync();
                var mappedItems = _mapper.Map<List<AnswerDTO>>(items);

                return new BaseResponse<List<AnswerDTO>>
                    (data: mappedItems, message: Messages.RetrievedSuccessfully, errors: [], true);
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<AnswerDTO>>
                    (null, Messages.Error, new List<string> { ex.Message }, false);
            }
        }
    }
}
