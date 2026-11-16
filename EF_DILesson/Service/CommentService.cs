using EF_DILesson.Interfaces;
using EF_DILesson.Models;

namespace EF_DILesson.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IFilmRepository _filmRepository;

        public CommentService(ICommentRepository commentRepository, IFilmRepository filmRepository)
        {
            _commentRepository = commentRepository;
            _filmRepository = filmRepository;
        }

        public List<MovieComment> GetCommentsByFilm(int filmId)
        {
            Film film = _filmRepository.GetById(filmId);
            if (film == null) return null;

            return _commentRepository.GetByFilmId(filmId);
        }

        public MovieComment AddComment(int filmId, MovieComment comment)
        {
            Film film = _filmRepository.GetById(filmId);
            if (film == null) return null;

            comment.MovieId = filmId;
            comment.CreatedAt = DateTime.UtcNow;

            _commentRepository.Add(comment);
            return comment;
        }

        public MovieComment UpdateComment(int id, MovieComment newData)
        {
            MovieComment comment = _commentRepository.GetById(id);
            if (comment == null) return null;

            comment.Text = newData.Text;

            _commentRepository.Update(comment);
            return comment;
        }

        public bool DeleteComment(int id)
        {
            return _commentRepository.Delete(id);
        }
    }
}