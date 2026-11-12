using EF_DILesson.Models;

namespace EF_DILesson.Repository
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        private bool UserExists(string login)
        {
            var user = _context.Users.FirstOrDefault(x => x.Login == login);
            if (user == null) return false;
            return true;
        }

        public void RegisterUser(User user)
        {
            if (!UserExists(user.Login))
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            else throw new Exception("Користувач з таким логіном вже існує");
        }

        public void LoginUser(User user)
        {
            if (!UserExists(user.Login))
            {
                throw new Exception("Користувача з таким логіном не знайдено");
            }

            var userValidate = _context.Users.FirstOrDefault(x => x.Login == user.Login);
            if (userValidate.Password != user.Password) throw new Exception("Неправильний пароль");
        }
    }
}