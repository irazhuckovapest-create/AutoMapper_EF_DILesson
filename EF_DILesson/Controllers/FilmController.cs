using EF_DILesson.Dto;
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

        [HttpGet]
        public ActionResult<List<FilmDto>> GetAll()
        {
            return Ok(_filmService.GetAllFilms());
        }

        [HttpPost]
        public ActionResult<string> Add(FilmCreateDto film)
        {
            _filmService.AddFilm(film);
            return Ok("Фільм додано");
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            bool deleted = _filmService.DeleteFilm(id);
            if (deleted) return Ok("Фільм видалено");
            else return NotFound("Фільм не знайдено");
        }
    }
}