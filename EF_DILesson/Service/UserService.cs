using EF_DILesson.Dto;
using EF_DILesson.Models;
using EF_DILesson.Repository;

namespace EF_DILesson.Service
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Result RegisterUser(UserRegisterDto dto)
        {
            Result result = new Result();

            try
            {
                User user = new User();
                user.Login = dto.Login;
                user.Password = dto.Password;
                user.Name = dto.Name;
                user.Surname = dto.Surname;
                user.Age = dto.Age;

                _userRepository.RegisterUser(user);
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public Result LoginUser(UserLoginDto dto)
        {
            Result result = new Result();

            try
            {
                User user = new User();
                user.Login = dto.Login;
                user.Password = dto.Password;

                _userRepository.LoginUser(user);
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}