using System.ComponentModel.DataAnnotations;

namespace EF_DILesson.Dto
{
    public class UserLoginDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}