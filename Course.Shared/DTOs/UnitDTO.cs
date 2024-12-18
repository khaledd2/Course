using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Shared.DTOs
{
    public class UnitDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Order {  get; set; }
        public bool IsLocked { get; set; }

        public int CourseId { get; set; } 
    }
}
