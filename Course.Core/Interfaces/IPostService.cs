using Course.Shared.DTOs;
using Course.Shared.Records;
using Course.Shared.ViewModels;
using Course.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course.Shared.Interfaces;

namespace Course.BLL.Interfaces
{
    public interface IPostService
    {
        Task<BaseResponse<DataTableVM<GetPostDTO>>> GetAllPostsAsync(IPagination pagination);
        Task<BaseResponse<GetPostDTO>> GetPostByIdAsync(int id);
        Task<BaseResponse<PostPostDTO>> UpdatePostAsync(PostPostDTO post);
        Task<BaseResponse<PostPostDTO>> CreatePostAsync(PostPostDTO post);
        Task<BaseResponse<GetPostDTO>> RemovePostAsync(int postId);
    }
}
