using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Shared.DTOs
{
    public class GetSurveyDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string UnitName { get; set; } = null!;

        public string CourseTitle { get; set; } = null!;

        public int Order { get; set; }

        public int UnitId { get; set; }

        public List<GetQuestionDTO> Questions = new List<GetQuestionDTO>();
    }

    public partial class PostSurveyDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "الرجاء إدخال عنوان الاختبار")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "الرجاء إدخال ترتيب الاختبار")]
        public int Order { get; set; }

        [Required(ErrorMessage = "الرجاء اختيار وحدة")]
        public int UnitId { get; set; }
    }
}
