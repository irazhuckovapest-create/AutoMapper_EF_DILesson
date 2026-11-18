using AutoMapper;
using EF_DILesson.Contracts;
using EF_DILesson.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EF_DILesson.MapperProfile
{
    public class RequestResponseProfile : Profile
    {
        public RequestResponseProfile()
        {
            CreateMap<CreateMovieRequest, Film>();
            CreateMap<UpdateMovieRequest, Film>();
            CreateMap<Film, MovieResponse>();

            CreateMap<CreateCommentRequest, MovieComment>();
            CreateMap<UpdateCommentRequest, MovieComment>();
            CreateMap<MovieComment, CommentResponse>();
        }
    }
}