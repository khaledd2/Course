using AutoMapper;
using Course.BLL.Extensions;
using Course.BLL.Interfaces;
using Course.DAL;
using Course.DAL.Models;
using Course.Shared;
using Course.Shared.Constants;
using Course.Shared.DTOs;
using Course.Shared.Interfaces;
using Course.Shared.Records;
using Course.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Services
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _db;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        public PostService(AppDbContext db, IImageService imageService, IMapper mapper)
        {
            _db = db;
            _imageService = imageService;
            _mapper = mapper;
        }

        public async Task<BaseResponse<DataTableVM<GetPostDTO>>> GetAllPostsAsync(IPagination pagination)
        {
            try
            {
                // Get course By id
                var itemsQuery = _db.Posts.Include(p=>p.Category).AsQueryable();

                // Search
                if (!string.IsNullOrWhiteSpace(pagination.Search))
                    itemsQuery = itemsQuery.Where(c => c.Title.Contains(pagination.Search) || c.Description.Contains(pagination.Search));

                // Size
                int dataSize = itemsQuery.Count();

                // Map
                var items = await itemsQuery
                .Skip(pagination.Skip())
                .Take(pagination.PageSize)
                .ToListAsync();

                var mappedItems = _mapper.Map<List<GetPostDTO>>(items);
                
                var dataTable = new DataTableVM<GetPostDTO>
                        (data: mappedItems, dataSize: dataSize, pageSize: pagination.PageSize, currentPage: pagination.PageNumber);

                return new BaseResponse<DataTableVM<GetPostDTO>>(dataTable, Messages.RetrievedSuccessfully, [], true);

            }
            catch (Exception ex)
            {
                return new BaseResponse<DataTableVM<GetPostDTO>>(null, Messages.Error, new List<string> { ex.Message }, false);

            }
        }

        public async Task<BaseResponse<PostPostDTO>> CreatePostAsync(PostPostDTO post)
        {
            try
            {

                // Add post image to /PostImages
                var imageResult = await _imageService.AddImageAsync(post.ImageFile, "PostImages/");
                if (!imageResult.Success)
                    return new BaseResponse<PostPostDTO>(null, imageResult.Message, [], false);

                // Set Id
                int maxPostId = 0;
                if (_db.Posts.Any())
                    maxPostId = await _db.Posts.MaxAsync(c => c.Id);

                // Create new post 
                var item = new Post()
                {
                    Id = maxPostId + 1,
                    Title = post.Title,
                    ImageUrl = imageResult.Data ?? "",
                    Description = post.Description,
                    CategoryId = post.CategoryId,
                    CreatedAt = DateTime.Now,
                };

                // Save changes
                await _db.AddAsync(item);
                await _db.SaveChangesAsync();
                post.Id = item.Id;
                return new BaseResponse<PostPostDTO>(post, Messages.AddedSuccessfully);
            }
            catch (Exception ex)
            {
                return new BaseResponse<PostPostDTO>(null, Messages.Error, new List<string> { ex.Message }, false);

            }
        }

        public async Task<BaseResponse<GetPostDTO>> GetPostByIdAsync(int id)
        {
            try
            {
                // Get post By id
                var item = await _db.Posts
                    .Include(c => c.Category)
                    .FirstOrDefaultAsync(p=> p.Id == id);

                if (item == null)
                    return new BaseResponse<GetPostDTO>(null, Messages.NotFound, [], false);

                var mappedItem = _mapper.Map<GetPostDTO>(item);
                return new BaseResponse<GetPostDTO>(mappedItem, Messages.RetrievedSuccessfully, [], true);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetPostDTO>(null, Messages.Error, new List<string> { ex.Message }, false);
            }
        }

        public async Task<BaseResponse<GetPostDTO>> RemovePostAsync(int postId)
        {
            try
            {
                // Get course to be removed
                var item = await _db.Posts.FirstOrDefaultAsync(c => c.Id == postId);
                if (item == null)
                    return new BaseResponse<GetPostDTO>(null, Messages.NotFound, [], false);

                // Remove
                _db.Posts.Remove(item);
                await _db.SaveChangesAsync();

                //
                await _imageService.RemoveImageAsync(item.ImageUrl);
                return new BaseResponse<GetPostDTO>(null, Messages.RemovedSuccessfully);

            }
            catch (Exception ex)
            {
                return new BaseResponse<GetPostDTO>(null, Messages.ReferenceError, new List<string> { ex.Message }, false);
            }
        }

        public async Task<BaseResponse<PostPostDTO>> UpdatePostAsync(PostPostDTO post)
        {
            try
            {
                // To get the old post image url
                var oldPost = await _db.Posts.AsNoTracking().FirstOrDefaultAsync(c => c.Id == post.Id);
                if (oldPost == null)
                    return new BaseResponse<PostPostDTO>(null, Messages.NotFound, [], false);

                // Add course image to /PostImages if user pass new image 
                var imageResult = new BaseResponse<string>(null, Messages.Error, [], false);
                string imageToBeDeleted = oldPost.ImageUrl;

                if (post.ImageFile != null)
                {
                    // Add new image 
                    imageResult = await _imageService.AddImageAsync(post.ImageFile, "PostImages/");
                    if (!imageResult.Success)
                        return new BaseResponse<PostPostDTO>(null, imageResult.Message, [], false);

                }

                // New post
                var newPost = _mapper.Map<Post>(post);
                newPost.CreatedAt = oldPost.CreatedAt;
                newPost.ImageUrl = imageResult.Data ?? "";

                _db.Posts.Update(newPost);
                await _db.SaveChangesAsync();

                // Remove the old image
                if (imageResult.Success)
                {
                    // To delete the old course image
                    var deleteImageResult = await _imageService.RemoveImageAsync(imageToBeDeleted);
                }

                return new BaseResponse<PostPostDTO>(post, Messages.UpdatedSuccessfully);
            }
            catch (Exception ex)
            {
                return new BaseResponse<PostPostDTO>(null, Messages.Error, new List<string> { ex.Message }, false);

            }
        }
    }
}
