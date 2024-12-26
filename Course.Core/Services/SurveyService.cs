using AutoMapper;
using Course.BLL.Extensions;
using Course.BLL.Interfaces;
using Course.DAL;
using Course.DAL.Migrations;
using Course.DAL.Models;
using Course.Shared;
using Course.Shared.Constants;
using Course.Shared.DTOs;
using Course.Shared.Interfaces;
using Course.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly IQuestionService _questionService;
        public SurveyService(AppDbContext db, IMapper mapper, IQuestionService questionService)
        {
            _db = db;
            _mapper = mapper;
            _questionService = questionService;
        }
        
        public async Task<BaseResponse<PostSurveyDTO>> CreateSurveyAsync(PostSurveyDTO survey)
        {
            try
            {
                int maxSurveyId = 0;

                // Set Id
                if (_db.Surveys.Any())
                    maxSurveyId = await _db.Surveys.MaxAsync(c => c.Id);

                // Create new survey 
                var item = _mapper.Map<Survey>(survey);
                item.Id = maxSurveyId + 1;

                // Save changes
                await _db.AddAsync(item);
                await _db.SaveChangesAsync();

                survey.Id = item.Id;
                return new BaseResponse<PostSurveyDTO>(survey, Messages.AddedSuccessfully);
            }
            catch (Exception ex)
            {
                return new BaseResponse<PostSurveyDTO>(null, Messages.Error, new List<string> { ex.Message }, false);

            }
        }

        public async Task<BaseResponse<DataTableVM<GetSurveyDTO>>> GetAllSurveysAsync(IPagination pagination)
        {
            try
            {
                // Get surveys
                var itemsQuery = _db.Surveys
                    .Include(s=>s.Unit)
                    .Include(s=>s.Unit.Course)
                    .AsQueryable();

                // Search
                if (!string.IsNullOrWhiteSpace(pagination.Search))
                    itemsQuery = itemsQuery.Where(c => c.Name.Contains(pagination.Search));

                // Size
                int dataSize = itemsQuery.Count();

                // Items
                var items = await itemsQuery
                    .Skip(pagination.Skip())
                    .Take(pagination.PageSize)
                    .ToListAsync();

                // Mapped items
                var mappedItems = _mapper.Map<List<GetSurveyDTO>>(items);
               
                // Get relation questions
                foreach(var item in mappedItems)
                {
                    var questionResult = await _questionService.GetAllQuestionsBySurveyIdAsync(item.Id);

                    if (questionResult.Success)
                        item.Questions = questionResult.Data ?? new();
                }

                var dataTable = new DataTableVM<GetSurveyDTO>
                        (data: mappedItems, dataSize: dataSize, pageSize: pagination.PageSize, currentPage: pagination.PageNumber);

                return new BaseResponse<DataTableVM<GetSurveyDTO>>(dataTable, Messages.RetrievedSuccessfully, [], true);
            }
            catch (Exception ex)
            {
                return new BaseResponse<DataTableVM<GetSurveyDTO>>(null, Messages.Error, new List<string> { ex.Message }, false);
            }
        }

        public async Task<BaseResponse<GetSurveyDTO>> GetSurveyByIdAsync(int id)
        {
            try
            {
                // Get course By id
                var item = await _db.Surveys
                    .Include(s=>s.Unit)
                    .Include(s=>s.Unit.Course)
                    .FirstOrDefaultAsync(c => c.Id == id);

                // Map
                var mappedItem = _mapper.Map<GetSurveyDTO>(item);

                //
                if (item == null)
                    return new BaseResponse<GetSurveyDTO>(null, Messages.NotFound, [], false);

                return new BaseResponse<GetSurveyDTO>(mappedItem, Messages.RetrievedSuccessfully, [], true);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetSurveyDTO>(null, Messages.Error, new List<string> { ex.Message }, false);
            }
        }

        public async Task<BaseResponse<GetSurveyDTO>> RemoveSurveyAsync(int surveyId)
        {
            try
            {
                // Get survey to be removed
                var item = await _db.Surveys.FindAsync(surveyId);
                if (item == null)
                    return new BaseResponse<GetSurveyDTO>(null, Messages.NotFound, [], false);

                // Remove
                _db.Surveys.Remove(item);
                await _db.SaveChangesAsync();

                return new BaseResponse<GetSurveyDTO>(null, Messages.RemovedSuccessfully);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetSurveyDTO>(null, Messages.ReferenceError, new List<string> { ex.Message }, false);
            }
        }

        public async Task<BaseResponse<PostSurveyDTO>> UpdateSurveyAsync(PostSurveyDTO survey)
        {
            try
            {
                if (!(await _db.Surveys.AnyAsync(s=>s.Id == survey.Id)))
                    return new BaseResponse<PostSurveyDTO>(null, Messages.NotFound, [], false);

                // Map
                var item = _mapper.Map<Survey>(survey);

                // Update
                _db.Surveys.Update(item);
                await _db.SaveChangesAsync();

                return new BaseResponse<PostSurveyDTO>(survey, Messages.UpdatedSuccessfully);
            }
            catch (Exception ex)
            {
                return new BaseResponse<PostSurveyDTO>(null, Messages.Error, new List<string> { ex.Message }, false);
            }
        }
    }
}
