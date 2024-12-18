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
    public class LessonService : ILessonService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public LessonService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<BaseResponse<PostLessonDTO>> CreateLessonAsync(PostLessonDTO lesson)
        {
            try
            {
                int maxLessonId = 0;

                if (_db.Lessons.Any())
                    maxLessonId = await _db.Lessons.MaxAsync(c => c.Id);

                //mapper
                var entity = _mapper.Map<Lesson>(lesson);
                entity.Id = maxLessonId + 1;

                //add
                _db.Lessons.Add(entity);
                await _db.SaveChangesAsync();

                return new BaseResponse<PostLessonDTO>(lesson, Messages.AddedSuccessfully);
            }
            catch (Exception ex)
            {
                return new BaseResponse<PostLessonDTO>(null, Messages.Error, new List<string> { ex.Message }, false);
            }
        }

        public async Task<BaseResponse<GetOneLessonDTO>> GetLessonByIdAsync(int id)
        {
            try
            {
                var entity = await _db.Lessons.Include(l=>l.Unit).FirstOrDefaultAsync(l=>l.Id == id);
                if (entity == null)
                {
                    return new BaseResponse<GetOneLessonDTO>(null, Messages.NotFound, [], false);
                }
                
                // mapper
                var lesson = _mapper.Map<GetOneLessonDTO>(entity);

                return new BaseResponse<GetOneLessonDTO>(lesson, Messages.RetrievedSuccessfully);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetOneLessonDTO>(null, Messages.Error, new List<string> { ex.Message }, false);
            }
        }

        public async Task<BaseResponse<GetOneLessonDTO>> RemoveLessonAsync(int lessonId)
        {
            try
            {
                var entity = await _db.Lessons.FindAsync(lessonId);
                if (entity == null)
                    return new BaseResponse<GetOneLessonDTO>(null, Messages.NotFound, [], false);
                
                _db.Lessons.Remove(entity);
                await _db.SaveChangesAsync();

                return new BaseResponse<GetOneLessonDTO>(null, Messages.RemovedSuccessfully);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetOneLessonDTO>(null, Messages.Error, new List<string> { ex.Message }, false);
            }
        }

        public async Task<BaseResponse<PostLessonDTO>> UpdateLessonAsync(PostLessonDTO lesson)
        {
            try
            {
                var entity = await _db.Lessons.AsNoTracking().FirstOrDefaultAsync(l=>l.Id == lesson.Id);
                if (entity == null)
                {
                    return new BaseResponse<PostLessonDTO>(null, Messages.NotFound, [], false);
                }

                entity = _mapper.Map<Lesson>(lesson);

                _db.Lessons.Update(entity);
                await _db.SaveChangesAsync();

                return new BaseResponse<PostLessonDTO>(lesson, Messages.UpdatedSuccessfully);
            }
            catch (Exception ex)
            {
                return new BaseResponse<PostLessonDTO>(null, Messages.Error, new List<string> { ex.Message }, false);
            }
        }

    }
}
