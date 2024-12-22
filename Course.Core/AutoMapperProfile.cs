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
            // Lesson
            CreateMap<Lesson, GetOneLessonDTO>().ReverseMap();
            CreateMap<Lesson, PostLessonDTO>().ReverseMap();
            CreateMap<Lesson, GetAllLessonsDTO>().ReverseMap();

            // Post
            CreateMap<Post, PostPostDTO>().ReverseMap();
            CreateMap<Post,  GetPostDTO>()
                .ForMember(des=>des.CategoryName, opt=>opt.MapFrom(src=>src.Category.Name))
                .ForMember(des=>des.ImageUrl, opt=>opt.MapFrom(src=> src.ImageUrl == null ? "" : src.ImageUrl.Replace("wwwroot/", "")))
                .ReverseMap();
            
        }
    }
}
