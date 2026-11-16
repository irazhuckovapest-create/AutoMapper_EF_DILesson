using EF_DILesson.Models;

namespace EF_DILesson.Interfaces
{
    public interface IFilmService
    {
        List<Film> GetAllFilms();
        Film GetFilmById(int id);
        void AddFilm(Film film);
        Film UpdateFilm(int id, Film newData);
        bool DeleteFilm(int id);
    }
}