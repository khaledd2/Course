using Course.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Course.BLL.Interfaces
{
    public interface IImageService
    {
        Task<BaseResponse<string>> AddImageAsync(IFormFile? file, string filePath);
        Task<BaseResponse<string>> RemoveImageAsync(string filePath);
    }
}
