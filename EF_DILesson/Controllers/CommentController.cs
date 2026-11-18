using AutoMapper;
using EF_DILesson.Contracts;
using EF_DILesson.Interfaces;
using EF_DILesson.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EF_DILesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IValidator<CreateCommentRequest> _createValidator;
        private readonly IValidator<UpdateCommentRequest> _updateValidator;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService,
            IValidator<CreateCommentRequest> createValidator,
            IValidator<UpdateCommentRequest> updateValidator,
            IMapper mapper)
        {
            _commentService = commentService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _mapper = mapper;
        }

        [HttpGet("film/{filmId}")]
        public ActionResult<List<CommentResponse>> GetByFilm(int filmId)
        {
            var comments = _commentService.GetCommentsByFilm(filmId);
            if (comments == null) return NotFound("Фільм з id " + filmId + " не знайдено");

            return Ok(_mapper.Map<List<CommentResponse>>(comments));
        }

        [HttpPost("film/{filmId}")]
        public ActionResult<CommentResponse> Create(int filmId, CreateCommentRequest request)
        {
            var res = _createValidator.Validate(request);
            if (!res.IsValid) return BadRequest(res.Errors);

            MovieComment comment = _mapper.Map<MovieComment>(request);

            MovieComment created = _commentService.AddComment(filmId, comment);
            if (created == null) return NotFound("Фільм з id " + filmId + " не знайдено");

            return Ok(_mapper.Map<CommentResponse>(created));
        }

        [HttpPut("{id}")]
        public ActionResult<CommentResponse> Update(int id, UpdateCommentRequest request)
        {
            var res = _updateValidator.Validate(request);
            if (!res.IsValid) return BadRequest(res.Errors);

            MovieComment newData = _mapper.Map<MovieComment>(request);

            MovieComment comment = _commentService.UpdateComment(id, newData);
            if (comment == null) return NotFound("Коментар з id " + id + " не знайдено");

            return Ok(_mapper.Map<CommentResponse>(comment));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            bool deleted = _commentService.DeleteComment(id);
            if (!deleted) return NotFound("Коментар з id " + id + " не знайдено");
            return NoContent();
        }
    }
}