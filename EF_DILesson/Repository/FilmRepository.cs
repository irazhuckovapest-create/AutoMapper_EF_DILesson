using EF_DILesson.Models;

namespace EF_DILesson.Repository
{
    public class FilmRepository
    {
        private readonly AppDbContext _context;

        public FilmRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Film> GetAll()
        {
            return _context.Films.ToList();
        }

        public void Add(Film film)
        {
            _context.Films.Add(film);
            _context.SaveChanges();
        }

        public bool Delete(int id)
        {
            var film = _context.Films.FirstOrDefault(f => f.Id == id);
            if (film == null) return false;
            _context.Films.Remove(film);
            _context.SaveChanges();
            return true;
        }
    }
}