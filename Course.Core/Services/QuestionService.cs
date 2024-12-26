using AutoMapper;
using Course.BLL.Interfaces;
using Course.DAL;
using Course.DAL.Migrations;
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
    public class QuestionService : IQuestionService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly IAnswerService _answerService;
        public QuestionService(AppDbContext db, IMapper mapper, IAnswerService answerService)
        {
            _db = db;
            _mapper = mapper;
            _answerService = answerService;
        }

        public async Task<BaseResponse<List<GetQuestionDTO>>> GetAllQuestionsBySurveyIdAsync(int surveyId)
        {
            try
            {
                var items = _db.Questions.Where(q => q.SurveyId == surveyId).ToListAsync();
                var mappedItems = _mapper.Map<List<GetQuestionDTO>>(items);

                foreach(var item in mappedItems)
                {
                    var answersResult = (await _answerService.GetAllAnswersByQuestionIdAsync(item.Id));

                    if (answersResult.Success)
                        item.Answers = answersResult.Data ?? new();
                }

                return new BaseResponse<List<GetQuestionDTO>>
                    (data: mappedItems, message: Messages.RetrievedSuccessfully, errors: [], true);
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<GetQuestionDTO>>
                    (null, Messages.Error, new List<string> { ex.Message }, false);
            }
        }

        public async Task<BaseResponse<PostQuestionDTO>> CreateQuestionAsync(PostQuestionDTO question)
        {
            try
            {
                int maxQuestionId = 0;
                if (_db.Questions.Any())
                    maxQuestionId = await _db.Questions.MaxAsync(c => c.Id);

                // Map
                var entity = _mapper.Map<Question>(question);

                // Save
                _db.Questions.Add(entity);
                await _db.SaveChangesAsync();

                question.Id = entity.Id;
                return new BaseResponse<PostQuestionDTO>(question, Messages.AddedSuccessfully);
            }
            catch (Exception ex)
            {
                return new BaseResponse<PostQuestionDTO>(null, Messages.Error, new List<string> { ex.Message }, false);
            }
        }

        public async Task<BaseResponse<PostQuestionDTO>> RemoveQuestionAsync(int questionId)
        {
            try
            {
                var entity = await _db.Questions.FindAsync(questionId);
                if (entity == null)
                    return new BaseResponse<PostQuestionDTO>(null, Messages.NotFound, [], false);

                _db.Questions.Remove(entity);
                await _db.SaveChangesAsync();

                return new BaseResponse<PostQuestionDTO>(null, Messages.RemovedSuccessfully);
            }
            catch (Exception ex)
            {
                return new BaseResponse<PostQuestionDTO>(null, Messages.Error, new List<string> { ex.Message }, false);
            }
        }

        public async Task<BaseResponse<PostQuestionDTO>> UpdateQuestionAsync(PostQuestionDTO question)
        {
            try
            {
                if (!(await _db.Questions.AnyAsync(s => s.Id == question.Id)))
                    return new BaseResponse<PostQuestionDTO>(null, Messages.NotFound, [], false);

                // Map
                var item = _mapper.Map<Question>(question);

                // Update
                _db.Questions.Update(item);
                await _db.SaveChangesAsync();

                return new BaseResponse<PostQuestionDTO>(question, Messages.UpdatedSuccessfully);
            }
            catch (Exception ex)
            {
                return new BaseResponse<PostQuestionDTO>(null, Messages.Error, new List<string> { ex.Message }, false);
            }
        }
    }
}
