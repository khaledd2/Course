using System;
using System.Collections.Generic;
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

        public string CourseName { get; set; } = null!;

        public int Order { get; set; }

        public int UnitId { get; set; }
    }

    public partial class PostSurveyDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Order { get; set; }

        public int UnitId { get; set; }
    }
}
