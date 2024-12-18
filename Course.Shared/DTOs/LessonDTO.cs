using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Course.Shared.DTOs
{
    public class PostLessonDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Script { get; set; } = null!;

        public string VideoUrl { get; set; } = null!;

        public int UnitId { get; set; }

        public int Order { get; set; }

    }
    public class GetOneLessonDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Script { get; set; } = null!;

        public string VideoUrl { get; set; } = null!;

        public int UnitId { get; set; }

        public string UnitName { get; set; } = string.Empty;

        public int Order { get; set; }

    }
    public class GetAllLessonsDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int UnitId { get; set; }

        public string UnitName { get; set; } = string.Empty;

        public int Order { get; set; }

    }

}
