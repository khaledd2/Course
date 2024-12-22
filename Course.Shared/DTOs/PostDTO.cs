using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Shared.DTOs
{
    public class PostPostDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "الرجاء إدخال عنوان المقالة")]
        [StringLength(100, ErrorMessage = "يجب أن يكون عنوان المقالة أقل من 100 حرف")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "الرجاء إدخال صورة المقالة")]
        public IFormFile ImageFile { get; set; } = null!;

        [Required(ErrorMessage = "الرجاء إدخال وصف المقالة")]
        [StringLength(15000, ErrorMessage = "يجب أن تكون المقالة أقل من 15000 حرف")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "الرجاء اختيار فئة المقالة")]
        public int CategoryId { get; set; }
    }


    public class GetPostDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public string Description { get; set; } = null!;

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
