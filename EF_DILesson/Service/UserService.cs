using AutoMapper;
using EF_DILesson.Dto;
using EF_DILesson.Models;
using EF_DILesson.Repository;

namespace EF_DILesson.Service
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(UserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Result RegisterUser(UserRegisterDto dto)
        {
            Result result = new Result();

            try
            {
                User user = _mapper.Map<User>(dto);

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
                User user = _mapper.Map<User>(dto);

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