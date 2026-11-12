using System.ComponentModel.DataAnnotations;

namespace EF_DILesson.Dto
{
    public class FilmCreateDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}