using Course.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace Course.Shared.Records
{
    public record Pagination : IPagination
    {
        [Range(1, 300, ErrorMessage = "تخطيت الحد المسموح به من الصفحات")]
        public int PageNumber { get; set; } = 1;
        [Range(1, 20, ErrorMessage = "تخطيت الحد المسموح به لحجم الصفحة")]
        public virtual int PageSize { get; set; } = 10;
        public string Search {  get; set; } = string.Empty;
        public int Skip() => (PageNumber - 1) * PageSize;
    }
}
