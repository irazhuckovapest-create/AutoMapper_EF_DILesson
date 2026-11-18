using AutoMapper;
using EF_DILesson.Dto;
using EF_DILesson.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EF_DILesson.MapperProfile
{
    public class DtoMapperProfile : Profile
    {
        public DtoMapperProfile()
        {
            CreateMap<UserRegisterDto, User>();
            CreateMap<UserLoginDto, User>();
        }
    }
}