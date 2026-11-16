using EF_DILesson.Interfaces;
using EF_DILesson.Models;

namespace EF_DILesson.Service
{
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;

        public FilmService(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public List<Film> GetAllFilms()
        {
            return _filmRepository.GetAll();
        }

        public Film GetFilmById(int id)
        {
            return _filmRepository.GetById(id);
        }

        public void AddFilm(Film film)
        {
            _filmRepository.Add(film);
        }

        public Film UpdateFilm(int id, Film newData)
        {
            Film film = _filmRepository.GetById(id);
            if (film == null) return null;

            film.Title = newData.Title;
            film.Description = newData.Description;
            film.PosterUrl = newData.PosterUrl;
            film.MovieUrl = newData.MovieUrl;
            film.ReleaseYear = newData.ReleaseYear;
            film.Rating = newData.Rating;

            _filmRepository.Update(film);
            return film;
        }

        public bool DeleteFilm(int id)
        {
            return _filmRepository.Delete(id);
        }
    }
}