using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Shared.DTOs
{
    //public class PostCourseDTO
    //{
    //    [Required(ErrorMessage = "الرجاء إدخال عنوان الدورة")]
    //    public int Id { get; set; }

    //    [Required(ErrorMessage = "الرجاء إدخال عنوان الدورة")]
    //    [StringLength(100, ErrorMessage = "يجب أن يكون عنوان الدورة أقل من 100 حرف")]
    //    public string Title { get; set; } = null!;

    //    [Required(ErrorMessage = "الرجاء إدخال وصف الدورة")]
    //    [StringLength(500, ErrorMessage = "يجب أن يكون وصف الدورة أقل من 500 حرف")]
    //    public string Description { get; set; } = null!;

    //    public IFormFile? ImageFile { get; set; } = null!;

    //    [Required(ErrorMessage = "الرجاء تحديد ما إذا كانت الدورة توفر شهادة")]
    //    public bool HasCertificate { get; set; }

    //    [Required(ErrorMessage = "الرجاء إدخال سؤال الدورة")]
    //    public string Question { get; set; } = null!;

    //    [Required(ErrorMessage = "الرجاء إدخال إجابة الدورة")]
    //    public string Answer { get; set; } = null!;

    //    [Required(ErrorMessage = "الرجاء اختيار فئة الدورة")]
    //    public int CategoryId { get; set; }

    //    [Required(ErrorMessage = "الرجاء تحديد ما إذا كان بإمكان تنزيل الدورة")]
    //    public bool AllowDownload { get; set; }

    //    [Required(ErrorMessage = "الرجاء إدخال أهداف الدورة")]
    //    public string Goals { get; set; } = null!;
    //}

    public class PostCourseDTO
    {
        [Required(ErrorMessage = "الرجاء إدخال عنوان الدورة")]
        public int Id { get; set; }

        [Required(ErrorMessage = "الرجاء إدخال عنوان الدورة")]
        [StringLength(100, ErrorMessage = "يجب أن يكون عنوان الدورة أقل من 100 حرف")]
        public string Title { get; set; } = "Default Title";

        [Required(ErrorMessage = "الرجاء إدخال وصف الدورة")]
        [StringLength(500, ErrorMessage = "يجب أن يكون وصف الدورة أقل من 500 حرف")]
        public string Description { get; set; } = "Default Description";

        public IFormFile? ImageFile { get; set; } = null;

        [Required(ErrorMessage = "الرجاء تحديد ما إذا كانت الدورة توفر شهادة")]
        public bool HasCertificate { get; set; } = false;

        [Required(ErrorMessage = "الرجاء إدخال سؤال الدورة")]
        public string Question { get; set; } = "Default Question";

        [Required(ErrorMessage = "الرجاء إدخال إجابة الدورة")]
        public string Answer { get; set; } = "Default Answer";

        [Required(ErrorMessage = "الرجاء اختيار فئة الدورة")]
        public int CategoryId { get; set; } = 1;

        [Required(ErrorMessage = "الرجاء تحديد ما إذا كان بإمكان تنزيل الدورة")]
        public bool AllowDownload { get; set; } = true;

        [Required(ErrorMessage = "الرجاء إدخال أهداف الدورة")]
        public string Goals { get; set; } = "Default Goals";
    }

    public class GetOneCourseDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public bool HasCertificate { get; set; }

        public string Question { get; set; } = null!;

        public string Answer { get; set; } = null!;

        public int CategoryId { get; set; }

        public bool AllowDownload { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CategoryName { get; set; } = null!;

        public virtual ICollection<GoalDTO> Goals { get; set; } = new List<GoalDTO>();

        public virtual ICollection<UnitDTO> Units { get; set; } = new List<UnitDTO>();
    }

    public class GetAllCoursesDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public bool AllowDownload { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CategoryName { get; set; } = null!;

    }
}
