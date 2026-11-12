using EF_DILesson.Dto;
using EF_DILesson.Models;
using EF_DILesson.Service;
using Microsoft.AspNetCore.Mvc;

namespace EF_DILesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public ActionResult<string> Login(UserLoginDto user)
        {
            Result result = _userService.LoginUser(user);
            if (result.IsSuccess) return Ok("Вхід виконано успішно");
            else return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public ActionResult<string> Register(UserRegisterDto user)
        {
            Result result = _userService.RegisterUser(user);
            if (result.IsSuccess) return Ok("Реєстрацію виконано успішно");
            else return BadRequest(result.Message);
        }
    }
}