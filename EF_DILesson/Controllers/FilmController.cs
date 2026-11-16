using EF_DILesson.Contracts;
using EF_DILesson.Interfaces;
using EF_DILesson.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EF_DILesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IFilmService _filmService;
        private readonly IValidator<CreateMovieRequest> _createValidator;
        private readonly IValidator<UpdateMovieRequest> _updateValidator;

        public FilmController(IFilmService filmService,
            IValidator<CreateMovieRequest> createValidator,
            IValidator<UpdateMovieRequest> updateValidator)
        {
            _filmService = filmService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        private MovieResponse ToResponse(Film film)
        {
            MovieResponse response = new MovieResponse();
            response.Id = film.Id;
            response.Title = film.Title;
            response.Description = film.Description;
            response.PosterUrl = film.PosterUrl;
            response.MovieUrl = film.MovieUrl;
            response.ReleaseYear = film.ReleaseYear;
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
            var res = _createValidator.Validate(request);
            if (!res.IsValid) return BadRequest(res.Errors);

            Film film = new Film();
            film.Title = request.Title;
            film.Description = request.Description;
            film.PosterUrl = request.PosterUrl;
            film.MovieUrl = request.MovieUrl;
            film.ReleaseYear = request.ReleaseYear;
            film.Rating = request.Rating;

            _filmService.AddFilm(film);
            return CreatedAtAction(nameof(GetById), new { id = film.Id }, ToResponse(film));
        }

        [HttpPut("{id}")]
        public ActionResult<MovieResponse> Update(int id, UpdateMovieRequest request)
        {
            var res = _updateValidator.Validate(request);
            if (!res.IsValid) return BadRequest(res.Errors);

            Film newData = new Film();
            newData.Title = request.Title;
            newData.Description = request.Description;
            newData.PosterUrl = request.PosterUrl;
            newData.MovieUrl = request.MovieUrl;
            newData.ReleaseYear = request.ReleaseYear;
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