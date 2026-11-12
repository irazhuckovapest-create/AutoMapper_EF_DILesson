using System.ComponentModel.DataAnnotations;

namespace EF_DILesson.Dto
{
    public class UserRegisterDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        public string Password { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(200)]
        public string Surname { get; set; }

        [Range(18, 120)]
        public int Age { get; set; }
    }
}