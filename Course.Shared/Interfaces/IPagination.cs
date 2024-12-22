using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Shared.Interfaces
{
    public interface IPagination
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        string Search { get; set; } 
        int Skip();
    }
}
