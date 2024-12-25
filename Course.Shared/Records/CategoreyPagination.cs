using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Shared.Records
{
    public record CategoreyPagination : Pagination
    {
        [Range(1, int.MaxValue, ErrorMessage = "تخطيت الحد المسموح به لحجم الصفحة")]
        public override int PageSize { get; set; } = 10;
    }
}
