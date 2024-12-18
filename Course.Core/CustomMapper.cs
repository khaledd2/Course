using Course.DAL.Models;
using Course.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL
{
    public static class CustomMapper
    {
        public static Lesson PostLessonDtoToLessonMapper(this Lesson lesson, PostLessonDTO Dto)
        {
            lesson.Name = Dto.Name;
            lesson.Script = Dto.Script;
            lesson.VideoUrl = Dto.VideoUrl;
            lesson.UnitId = Dto.UnitId;
            lesson.Order = Dto.Order;

            return lesson;
        }

        public static GetOneLessonDTO LessonToGetLessonDTOMapper(this GetOneLessonDTO dto, Lesson lesson) 
        { 
            dto.Id = lesson.Id;
            dto.Name = lesson.Name;
            dto.Script = lesson.Script;
            dto.VideoUrl = lesson.VideoUrl;
            dto.UnitId = lesson.UnitId;
            dto.UnitName = lesson.Unit.Name;
            dto.Order = lesson.Order;
            return dto;
        }
    }
}
