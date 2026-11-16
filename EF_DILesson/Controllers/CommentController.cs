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

        public CommentController(ICommentService commentService,
            IValidator<CreateCommentRequest> createValidator,
            IValidator<UpdateCommentRequest> updateValidator)
        {
            _commentService = commentService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        private CommentResponse ToResponse(MovieComment comment)
        {
            CommentResponse response = new CommentResponse();
            response.Id = comment.Id;
            response.MovieId = comment.MovieId;
            response.AuthorName = comment.AuthorName;
            response.Text = comment.Text;
            response.CreatedAt = comment.CreatedAt;
            return response;
        }

        [HttpGet("film/{filmId}")]
        public ActionResult<List<CommentResponse>> GetByFilm(int filmId)
        {
            var comments = _commentService.GetCommentsByFilm(filmId);
            if (comments == null) return NotFound("Фільм з id " + filmId + " не знайдено");

            var response = new List<CommentResponse>();
            foreach (var comment in comments)
            {
                response.Add(ToResponse(comment));
            }

            return Ok(response);
        }

        [HttpPost("film/{filmId}")]
        public ActionResult<CommentResponse> Create(int filmId, CreateCommentRequest request)
        {
            var res = _createValidator.Validate(request);
            if (!res.IsValid) return BadRequest(res.Errors);

            MovieComment comment = new MovieComment();
            comment.AuthorName = request.AuthorName;
            comment.Text = request.Text;

            MovieComment created = _commentService.AddComment(filmId, comment);
            if (created == null) return NotFound("Фільм з id " + filmId + " не знайдено");

            return Ok(ToResponse(created));
        }

        [HttpPut("{id}")]
        public ActionResult<CommentResponse> Update(int id, UpdateCommentRequest request)
        {
            var res = _updateValidator.Validate(request);
            if (!res.IsValid) return BadRequest(res.Errors);

            MovieComment newData = new MovieComment();
            newData.Text = request.Text;

            MovieComment comment = _commentService.UpdateComment(id, newData);
            if (comment == null) return NotFound("Коментар з id " + id + " не знайдено");

            return Ok(ToResponse(comment));
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