using Course.BLL.Interfaces;
using Course.Shared;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course.Shared.Helpers;
namespace Course.BLL.Services
{
    public class ImageService : IImageService
    {
        private readonly long _maxFileSize = 5 * 1024 * 1024; // 5 MB
        private readonly List<string> _allowedExtensions = new() { ".jpg", ".jpeg", ".png"};
        public async Task<BaseResponse<string>> AddImageAsync(IFormFile file, string filePath)
        {
            try
            {
                if(file == null)
                    return new BaseResponse<string>(
                        null,
                        "الرجاء اختيار صورة",
                        new List<string>(),
                        false);

                // Validate file size
                if (file.Length > _maxFileSize)
                {
                    return new BaseResponse<string>(
                        null,
                        "حجم الملف أكبر من الحد المسموح به (5 ميجابايت).",
                        new List<string>(),
                        false);
                }

                // Validate file extension
                var fileExtension = Path.GetExtension(file.FileName);
                if (!_allowedExtensions.Contains(fileExtension.ToLower()))
                {
                    return new BaseResponse<string>(
                        null,
                        "نوع الملف غير مدعوم. الأنواع المسموح بها هي: .jpg, .jpeg, .png, .gif.",
                        new List<string> (),
                        false);
                }

                // Save file to the specified path
                filePath = "wwwroot//" + filePath + RandomStringGenerator.GenerateRandomString(12) + fileExtension;
                var imageName = Path.GetFileName(filePath);
                var directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory ??"");

                await using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                return new BaseResponse<string>(
                    filePath,
                    "تم رقع الصورة بنجاح",
                    [],
                    true);
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>(
                    null,
                    "حدث خطأ أثناء رفع الصورة",
                    new List<string> { ex.Message },
                    false);
            }
        }

        public async Task<BaseResponse<string>> RemoveImageAsync(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new BaseResponse<string>(
                        null,
                        "الملف غير موجود.",
                        new List<string> (),
                        false);
                }

                File.Delete(filePath);

                return new BaseResponse<string>(
                    filePath,
                    "تم حذف الصورة بنجاح",
                    [],
                    true);
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>(
                    null,
                    "حدث خطأ أثناء حذف الصورة",
                    new List<string> { ex.Message },
                    false);
            }
        }
    }
}