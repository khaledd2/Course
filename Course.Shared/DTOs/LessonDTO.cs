using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Course.Shared.DTOs
{
    using System.ComponentModel.DataAnnotations;

    public class PostLessonDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "الرجاء إدخال اسم الدرس")]
        [StringLength(100, ErrorMessage = "يجب أن يكون اسم الدرس أقل من 100 حرف")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "الرجاء إدخال نص الدرس")]
        [StringLength(10000, ErrorMessage = "يجب أن يكون نص الدرس أقل من 10000 حرف")]
        public string Script { get; set; } = null!;

        [Required(ErrorMessage = "الرجاء إدخال رابط الفيديو")]
        [Url(ErrorMessage = "الرجاء إدخال رابط فيديو صالح")]
        public string VideoUrl { get; set; } = null!;

        [Required(ErrorMessage = "الرجاء اختيار الوحدة")]
        public int UnitId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "الرجاء إدخال ترتيب صحيح")]
        public int Order { get; set; }

        public bool IsLocked { get; set; }
    }

    public class GetOneLessonDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Script { get; set; } = null!;

        public string VideoUrl { get; set; } = null!;

        public int UnitId { get; set; }

        public string UnitName { get; set; } = string.Empty;

        public int Order { get; set; }

        public bool IsLocked { get; set; }

    }
    public class GetAllLessonsDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int UnitId { get; set; }

        public string UnitName { get; set; } = string.Empty;

        public int Order { get; set; }
        public bool IsLocked { get; set; }


    }

}
