using EF_DILesson.Interfaces;
using EF_DILesson.Models;

namespace EF_DILesson.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<MovieComment> GetByFilmId(int filmId)
        {
            return _context.Comments.Where(c => c.MovieId == filmId).ToList();
        }

        public MovieComment GetById(int id)
        {
            return _context.Comments.FirstOrDefault(c => c.Id == id);
        }

        public void Add(MovieComment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public void Update(MovieComment comment)
        {
            _context.Comments.Update(comment);
            _context.SaveChanges();
        }

        public bool Delete(int id)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.Id == id);
            if (comment == null) return false;
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return true;
        }
    }
}