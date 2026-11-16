using EF_DILesson.Contracts;
using EF_DILesson.Models;
using EF_DILesson.Service;
using Microsoft.AspNetCore.Mvc;

namespace EF_DILesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly FilmService _filmService;

        public FilmController(FilmService filmService)
        {
            _filmService = filmService;
        }

        private MovieResponse ToResponse(Film film)
        {
            MovieResponse response = new MovieResponse();
            response.Id = film.Id;
            response.Title = film.Title;
            response.Description = film.Description;
            response.ImageUrl = film.ImageUrl;
            response.Year = film.Year;
            response.Rating = film.Rating;
            return response;
        }

        [HttpGet]
        public ActionResult<List<MovieResponse>> GetAll()
        {
            var films = _filmService.GetAllFilms();
            var response = new List<MovieResponse>();

            foreach (var film in films)
            {
                response.Add(ToResponse(film));
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult<MovieResponse> GetById(int id)
        {
            Film film = _filmService.GetFilmById(id);
            if (film == null) return NotFound("Фільм з id " + id + " не знайдено");
            return Ok(ToResponse(film));
        }

        [HttpPost]
        public ActionResult<MovieResponse> Create(CreateMovieRequest request)
        {
            Film film = new Film();
            film.Title = request.Title;
            film.Description = request.Description;
            film.ImageUrl = request.ImageUrl;
            film.Year = request.Year;
            film.Rating = request.Rating;

            _filmService.AddFilm(film);
            return CreatedAtAction(nameof(GetById), new { id = film.Id }, ToResponse(film));
        }

        [HttpPut("{id}")]
        public ActionResult<MovieResponse> Update(int id, UpdateMovieRequest request)
        {
            Film newData = new Film();
            newData.Title = request.Title;
            newData.Description = request.Description;
            newData.ImageUrl = request.ImageUrl;
            newData.Year = request.Year;
            newData.Rating = request.Rating;

            Film film = _filmService.UpdateFilm(id, newData);
            if (film == null) return NotFound("Фільм з id " + id + " не знайдено");
            return Ok(ToResponse(film));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            bool deleted = _filmService.DeleteFilm(id);
            if (!deleted) return NotFound("Фільм з id " + id + " не знайдено");
            return NoContent();
        }
    }
}