using EF_DILesson.Models;

namespace EF_DILesson.Interfaces
{
    public interface ICommentRepository
    {
        List<MovieComment> GetByFilmId(int filmId);
        MovieComment GetById(int id);
        void Add(MovieComment comment);
        void Update(MovieComment comment);
        bool Delete(int id);
    }
}