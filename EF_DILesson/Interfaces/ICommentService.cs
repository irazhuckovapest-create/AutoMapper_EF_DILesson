using EF_DILesson.Models;

namespace EF_DILesson.Interfaces
{
    public interface ICommentService
    {
        List<MovieComment> GetCommentsByFilm(int filmId);
        MovieComment AddComment(int filmId, MovieComment comment);
        MovieComment UpdateComment(int id, MovieComment newData);
        bool DeleteComment(int id);
    }
}