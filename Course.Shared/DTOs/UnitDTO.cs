using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Course.Shared.DTOs
{
    public class UnitDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "الرجاء إدخال اسم الوحدة")]
        [StringLength(100, ErrorMessage = "يجب أن يكون اسم الوحدة أقل من 100 حرف")]
        public string Name { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "الرجاء إدخال ترتيب صحيح")]
        public int Order { get; set; }

        public bool IsLocked { get; set; }

        [Required(ErrorMessage = "الرجاء اختيار الدورة")]
        public int CourseId { get; set; }
    }

    public class GetUnitNameDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }


}
