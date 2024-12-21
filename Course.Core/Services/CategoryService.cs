using Course.BLL.Interfaces;
using Course.Shared;
using Course.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course.DAL;
using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Course.Shared.Constants;
using Course.Shared.Records;
using Course.Shared.ViewModels;
namespace Course.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _db;

        public CategoryService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<BaseResponse<CategoryDTO>> CreateCategoryAsync(CategoryDTO category)
        {
            try
            {
                int maxCategoryId = 0;

                if (_db.Categories.Any()) 
                    maxCategoryId = await _db.Categories.MaxAsync(c => c.Id);

                var entity = new Category
                {
                    Id = maxCategoryId + 1,
                    Name = category.Name
                };

                _db.Categories.Add(entity);
                await _db.SaveChangesAsync();

                category.Id = entity.Id;
                return new BaseResponse<CategoryDTO>(category, Messages.AddedSuccessfully);
            }
            catch (Exception ex)
            {
                return new BaseResponse<CategoryDTO>(null, Messages.Error, new List<string> { ex.Message }, false);
            }
        }

        public async Task<BaseResponse<CategoryDTO>> UpdateCategoryAsync(CategoryDTO category)
        {
            try
            {
                var entity = await _db.Categories.FindAsync(category.Id);
                if (entity == null)
                {
                    return new BaseResponse<CategoryDTO>(null, Messages.NotFound, [], false);
                }

                entity.Name = category.Name;

                _db.Categories.Update(entity);
                await _db.SaveChangesAsync();

                return new BaseResponse<CategoryDTO>(category, Messages.UpdatedSuccessfully);
            }
            catch (Exception ex)
            {
                return new BaseResponse<CategoryDTO>(null, Messages.Error, new List<string> { ex.Message }, false);
            }
        }

        public async Task<BaseResponse<DataTableVM<CategoryDTO>>> GetAllCategoriesAsync(Pagination pagination)
        {
            try
            {
                var categoriesQuery = _db.Categories
                    .Select(c => new CategoryDTO
                    {
                        Id = c.Id,
                        Name = c.Name
                    });

                // Search
                if (!string.IsNullOrWhiteSpace(pagination.Search))
                    categoriesQuery = categoriesQuery.Where(c => c.Name.Contains(pagination.Search));

                // Data size
                int dataSize = categoriesQuery.Count();

                var categories = await
                    categoriesQuery
                    .Skip(pagination.Skip())
                    .Take(pagination.PageSize)
                    .ToListAsync();

                var dataTable = new DataTableVM<CategoryDTO>
                    (data: categories, dataSize: dataSize,pageSize: pagination.PageSize, currentPage: pagination.PageNumber );

                return new BaseResponse<DataTableVM<CategoryDTO>>(dataTable, Messages.RetrievedSuccessfully);
            }
            catch (Exception ex)
            {
                return new BaseResponse<DataTableVM<CategoryDTO>>(null, Messages.Error, new List<string> { ex.Message }, false);
            }
        }

        public async Task<BaseResponse<CategoryDTO>> GetCategoryByIdAsync(int id)
        {
            try
            {
                var entity = await _db.Categories.FindAsync(id);
                if (entity == null)
                {
                    return new BaseResponse<CategoryDTO>(null, Messages.NotFound, [], false);
                }

                var category = new CategoryDTO
                {
                    Id = entity.Id,
                    Name = entity.Name
                };

                return new BaseResponse<CategoryDTO>(category, Messages.RetrievedSuccessfully);
            }
            catch (Exception ex)
            {
                return new BaseResponse<CategoryDTO>(null, Messages.Error, new List<string> { ex.Message }, false);
            }
        }

        public async Task<BaseResponse<CategoryDTO>> RemoveCategoryAsync(int categoryId)
        {
            try
            {
                var entity = await _db.Categories.FindAsync(categoryId);
                if (entity == null)
                {
                    return new BaseResponse<CategoryDTO>(null, Messages.NotFound, [], false);
                }

                _db.Categories.Remove(entity);
                await _db.SaveChangesAsync();

                return new BaseResponse<CategoryDTO>(null, Messages.RemovedSuccessfully);
            }
            catch (Exception ex)
            {
                return new BaseResponse<CategoryDTO>(null, Messages.ReferenceError, new List<string> { ex.Message }, false);
            }
        }
    }

}
