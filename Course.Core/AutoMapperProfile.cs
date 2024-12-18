using AutoMapper;
using Course.DAL.Models;
using Course.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Lesson, GetOneLessonDTO>().ReverseMap();
            CreateMap<Lesson, PostLessonDTO>().ReverseMap();
        }
    }
}
