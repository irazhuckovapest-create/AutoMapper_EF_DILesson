using EF_DILesson.Dto;
using EF_DILesson.Models;
using EF_DILesson.Repository;

namespace EF_DILesson.Service
{
    public class FilmService
    {
        private readonly FilmRepository _filmRepository;

        public FilmService(FilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public List<FilmDto> GetAllFilms()
        {
            var films = _filmRepository.GetAll();
            var result = new List<FilmDto>();

            foreach (var film in films)
            {
                FilmDto dto = new FilmDto();
                dto.Id = film.Id;
                dto.Title = film.Title;
                dto.Description = film.Description;
                dto.ImageUrl = film.ImageUrl;
                result.Add(dto);
            }

            return result;
        }

        public void AddFilm(FilmCreateDto dto)
        {
            Film film = new Film();
            film.Title = dto.Title;
            film.Description = dto.Description;
            film.ImageUrl = dto.ImageUrl;
            _filmRepository.Add(film);
        }

        public bool DeleteFilm(int id)
        {
            return _filmRepository.Delete(id);
        }
    }
}