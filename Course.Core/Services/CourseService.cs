using Course.BLL.Extensions;
using Course.BLL.Interfaces;
using Course.DAL;
using Course.Shared;
using Course.Shared.Constants;
using Course.Shared.DTOs;
using Course.Shared.Interfaces;
using Course.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Course.BLL.Services
{
    public class CourseService : ICourseService
    {
        private readonly IImageService _imageService;
        private readonly AppDbContext _db;

        public CourseService(IImageService imageService, AppDbContext db)
        {
            _imageService = imageService;
            _db = db;
        }

        public async Task<BaseResponse<PostCourseDTO>> CreateCourseAsync(PostCourseDTO course)
        {
            try
            {
                // Add course image to /CourseImages
                var imageResult = await _imageService.AddImageAsync(course.ImageFile, "CourseImages/");
                if (!imageResult.Success)
                    return new BaseResponse<PostCourseDTO>(null, imageResult.Message, [], false);

                int maxCourseId = 0;
                int maxGoalId = 0;

                // Set Id
                if (_db.Courses.Any())
                    maxCourseId = await _db.Courses.MaxAsync(c => c.Id);
                if (_db.Goals.Any())
                    maxGoalId = await _db.Goals.MaxAsync(c => c.Id);

                // Create new course 
                var item = new DAL.Models.Course()
                {
                    Id = maxCourseId + 1,
                    Title = course.Title,
                    Description = course.Description,
                    ImageUrl = imageResult.Data ?? "",
                    HasCertificate = course.HasCertificate,
                    Question = course.Question,
                    Answer = course.Answer,
                    CategoryId = course.CategoryId,
                    AllowDownload = course.AllowDownload,
                    IsLocked = course.IsLocked,
                    CreatedAt = DateTime.Now,

                };

                // 
                item.Goals.ConvertGoalsFromStringToList(course.Goals, maxGoalId);

                // Save changes
                await _db.AddAsync(item);
                await _db.SaveChangesAsync();
                return new BaseResponse<PostCourseDTO>(course, Messages.AddedSuccessfully);

            }
            catch (Exception ex)
            {
                return new BaseResponse<PostCourseDTO>(null, Messages.Error, new List<string> { ex.Message }, false);

            }
        }

        public async Task<BaseResponse<DataTableVM<GetAllCourseNamesWithUnitNamesDTO>>> GetAllCourseNamesWithUnitNamesAsync(IPagination pagination)
        {
            try
            {
                // Get course By id
                var itemsQuery = _db.Courses.AsQueryable();

                // Search
                if (!string.IsNullOrWhiteSpace(pagination.Search))
                    itemsQuery = itemsQuery.Where(c => c.Title.Contains(pagination.Search) || c.Description.Contains(pagination.Search));

                // Size
                int dataSize = itemsQuery.Count();

                // Map
                var items = await itemsQuery
                    .Select(c => new GetAllCourseNamesWithUnitNamesDTO
                    {
                        Id = c.Id,
                        Title = c.Title,
                        Units = c.Units.Select(u => new GetUnitNameDTO { Id = u.Id, Name = u.Name }).ToList(),
                    })
                .Skip(pagination.Skip())
                .Take(pagination.PageSize)
                .ToListAsync();

                var dataTable = new DataTableVM<GetAllCourseNamesWithUnitNamesDTO>
                        (data: items, dataSize: dataSize, pageSize: pagination.PageSize, currentPage: pagination.PageNumber);

                return new BaseResponse<DataTableVM<GetAllCourseNamesWithUnitNamesDTO>>(dataTable, Messages.RetrievedSuccessfully, [], true);

            }
            catch (Exception ex)
            {
                return new BaseResponse<DataTableVM<GetAllCourseNamesWithUnitNamesDTO>>(null, Messages.Error, new List<string> { ex.Message }, false);

            }
        }

        public async Task<BaseResponse<DataTableVM<GetOneCourseDTO>>> GetAllCoursesAsync(IPagination pagination)
        {
            try
            {
                // Get course By id
                var itemsQuery = _db.Courses.AsQueryable();

                // Search
                if (!string.IsNullOrWhiteSpace(pagination.Search))
                    itemsQuery = itemsQuery.Where(c => c.Title.Contains(pagination.Search) || c.Description.Contains(pagination.Search));

                // Size
                int dataSize = itemsQuery.Count();

                // Map
                var items = await itemsQuery
                    .Select(c => new GetOneCourseDTO
                    {
                        Id = c.Id,
                        Title = c.Title,
                        Description = c.Description,
                        ImageUrl = c.ImageUrl == null ? "" : c.ImageUrl.Replace("wwwroot/", ""),
                        HasCertificate = c.HasCertificate,
                        Question = c.Question,
                        Answer = c.Answer,
                        CategoryId = c.CategoryId,
                        AllowDownload = c.AllowDownload,
                        CreatedAt = c.CreatedAt,
                        CategoryName = c.Category.Name,
                        IsLocked = c.IsLocked,


                        Goals = c.Goals.Select(g => new GoalDTO
                        {
                            Id = g.Id,
                            Name = g.Name
                        }).ToList(),

                        Units = c.Units.Select(u => new UnitDTO
                        {
                            Id = u.Id,
                            Name = u.Name,
                            IsLocked = u.IsLocked,
                        }).ToList()

                    })
                .Skip(pagination.Skip())
                .Take(pagination.PageSize)
                .ToListAsync();

                var dataTable = new DataTableVM<GetOneCourseDTO>
                        (data: items, dataSize: dataSize, pageSize: pagination.PageSize, currentPage: pagination.PageNumber);

                return new BaseResponse<DataTableVM<GetOneCourseDTO>>(dataTable, Messages.RetrievedSuccessfully, [], true);
            }
            catch (Exception ex)
            {
                return new BaseResponse<DataTableVM<GetOneCourseDTO>>(null, Messages.Error, new List<string> { ex.Message }, false);
            }

        }

        public async Task<BaseResponse<GetOneCourseDTO>> GetCourseByIdAsync(int id)
        {
            try
            {
                // Get course By id
                var item = await _db.Courses                
                    .Select(c => new GetOneCourseDTO
                    {
                        Id = c.Id,
                        Title = c.Title,
                        Description = c.Description,
                        ImageUrl = c.ImageUrl == null ? "" : c.ImageUrl.Replace("wwwroot/", ""),
                        HasCertificate = c.HasCertificate,
                        Question = c.Question,
                        Answer = c.Answer,
                        CategoryId = c.CategoryId,
                        AllowDownload = c.AllowDownload,
                        CreatedAt = c.CreatedAt,
                        CategoryName = c.Category.Name,
                        IsLocked = c.IsLocked,


                        Goals = c.Goals.Select(g => new GoalDTO
                        {
                            Id = g.Id,
                            Name = g.Name
                        }).ToList(),

                        Units = c.Units.Select(u => new UnitDTO
                        {
                            Id = u.Id,
                            Name = u.Name,
                            IsLocked = u.IsLocked,
                        }).ToList()

                    }).FirstOrDefaultAsync(c => c.Id == id);

                if (item == null)
                    return new BaseResponse<GetOneCourseDTO>(null, Messages.NotFound, [], false);

                // Incude lessons
                item.Lessons = await _db.Lessons
                    .Where(l => l.Unit.CourseId == id)
                    .OrderBy(l => l.UnitId)
                    .ThenBy(l => l.Order)
                    .Select(l => new GetAllLessonsDTO
                    {
                        Id = l.Id,
                        Name = l.Name,
                        Order = l.Order,
                        UnitId = l.UnitId,
                        UnitName = l.Unit.Name,
                        IsLocked = l.IsLocked,
                    }).ToListAsync();

                return new BaseResponse<GetOneCourseDTO>(item, Messages.RetrievedSuccessfully, [], true);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetOneCourseDTO>(null, Messages.Error, new List<string> { ex.Message }, false);
            }
        }
                
        public async Task<BaseResponse<GetOneCourseDTO>> RemoveCourseAsync(int courseId)
        {
            try
            {
                // Get course to be removed
                var item = await _db.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
                if (item == null)
                    return new BaseResponse<GetOneCourseDTO>(null, Messages.NotFound, [], false);

                // Remove
                _db.Courses.Remove(item);
                await _db.SaveChangesAsync();

                //
                await _imageService.RemoveImageAsync(item.ImageUrl);
                return new BaseResponse<GetOneCourseDTO>(null, Messages.RemovedSuccessfully);

            }
            catch (Exception ex)
            {
                return new BaseResponse<GetOneCourseDTO>(null, Messages.ReferenceError, new List<string> { ex.Message }, false);
            }
        }

        public async Task<BaseResponse<PostCourseDTO>> UpdateCourseAsync(PostCourseDTO course)
        {
            var imageResult = new BaseResponse<string>(null, Messages.Error, [], false);

            try
            {
                // To get the old course image data
                var oldCourse = await _db.Courses.Include(c => c.Goals).FirstOrDefaultAsync(c => c.Id == course.Id);
                if (oldCourse == null)
                    return new BaseResponse<PostCourseDTO>(null, Messages.NotFound, [], false);

                string imageToBeDeleted = oldCourse.ImageUrl;

                if (course.ImageFile != null)
                {
                    // Add new image 
                    imageResult = await _imageService.AddImageAsync(course.ImageFile, "CourseImages/");
                    if (!imageResult.Success)
                        return new BaseResponse<PostCourseDTO>(null, imageResult.Message, [], false);

                }

                // Update values
                oldCourse.Id = course.Id;
                oldCourse.Title = course.Title;
                oldCourse.Description = course.Description;
                oldCourse.ImageUrl = (imageResult.Success ? imageResult.Data : oldCourse.ImageUrl) ?? string.Empty;
                oldCourse.HasCertificate = course.HasCertificate;
                oldCourse.Question = course.Question;
                oldCourse.Answer = course.Answer;
                oldCourse.CategoryId = course.CategoryId;
                oldCourse.AllowDownload = course.AllowDownload;
                oldCourse.IsLocked = course.IsLocked;

                // clear the old goals
                oldCourse.Goals.Clear();

                // add new goals
                int maxGoalId = await _db.Goals.MaxAsync(c => c.Id);
                oldCourse.Goals.ConvertGoalsFromStringToList(course.Goals, maxGoalId);

                _db.Courses.Update(oldCourse);
                await _db.SaveChangesAsync();

                // Remove the old image
                if (imageResult.Success)
                    await _imageService.RemoveImageAsync(imageToBeDeleted);
                

                return new BaseResponse<PostCourseDTO>(course, Messages.UpdatedSuccessfully);
            }
            catch (Exception ex)
            {
                await _imageService.RemoveImageAsync(imageResult.Data ?? string.Empty);
                return new BaseResponse<PostCourseDTO>(null, Messages.Error, new List<string> { ex.Message }, false);
            }
        }

    }
}
