using System.ComponentModel.DataAnnotations;

namespace EF_DILesson.Contracts
{
    public class CreateMovieRequest
    {
        [Required(ErrorMessage = "Назва фільму обов'язкова")]
        [MaxLength(100, ErrorMessage = "Назва не може бути довшою за 100 символів")]
        public string Title { get; set; }

        [MaxLength(1000, ErrorMessage = "Опис не може бути довшим за 1000 символів")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Посилання на картинку обов'язкове")]
        [MaxLength(500, ErrorMessage = "Посилання не може бути довшим за 500 символів")]
        [RegularExpression(@"^(https?://|/).+", ErrorMessage = "Посилання має починатися з http://, https:// або /")]
        public string ImageUrl { get; set; }

        [Range(1888, 2100, ErrorMessage = "Рік має бути від 1888 до 2100")]
        public int Year { get; set; }

        [Range(0.0, 10.0, ErrorMessage = "Рейтинг має бути від 0 до 10")]
        public double Rating { get; set; }
    }
}