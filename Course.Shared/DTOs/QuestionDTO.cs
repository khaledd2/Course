using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Shared.DTOs
{
    public class GetQuestionDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Order { get; set; }

        public int SurveyId { get; set; }

        public virtual ICollection<AnswerDTO> Answers { get; set; } = new List<AnswerDTO>();
    }

    public class PostQuestionDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "الرجاء إدخال سؤال")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "الرجاء إدخال ترتيب السؤال")]
        public int Order { get; set; }

        [Required(ErrorMessage = "الرجاء اختيار سؤال")]
        public int SurveyId { get; set; }
    }
}
