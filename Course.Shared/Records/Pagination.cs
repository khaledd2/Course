using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Shared.Records
{
    public record Pagination
    {
        public int PageIndex { get; set; } = 1;
        [Range(1, 20)]
        public int PageSize { get; set; } = 10;
        public string Search {  get; set; } = string.Empty;
    }
}
