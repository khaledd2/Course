using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Shared.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "الرجاء إدخال اسم الصنف")]
        [StringLength(20, ErrorMessage = "يجب أن يكون اسم الصنف اقل من 20 حرف")]
        public string Name { get; set; } = null!;
    }
}
