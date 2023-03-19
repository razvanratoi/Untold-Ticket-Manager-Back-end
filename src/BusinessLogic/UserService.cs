using System.Text;
using Assignment1.Models;
using Assignment1.Repositories.Interfaces;
using Assignment1.Services;

namespace Assignment1.BusinessLogic
{
    public class UserService
    {
        private readonly IUserRepository _userRepo;
        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public User? GetUserById(int id)
        {
            return _userRepo.GetById(id).Result;
        }

        public User? GetUserByEmail(string email)
        {
            return _userRepo.GetUserByEmail(email).Result;
        }

        public List<User> GetAll()
        {
            return _userRepo.GetAll().Result.ToList();
        }

        public List<User> GetAllCashiers()
        {
            return _userRepo.GetAll().Result.Where(u => u.Role == 1).ToList();
        }

        public bool AddUser(User user)
        {
            user.Password = PasswordService.encryptPassword(user.Password);
            return _userRepo.Add(user).Result;
        }
    
        public bool UpdateUser(User user)
        {
            return _userRepo.Update(user).Result;
        }

        public bool DeleteUser(User user)
        {
            return _userRepo.Delete(user).Result;
        }

        public int LogIn(string username, string password)
        {
            var encryptedPass = PasswordService.encryptPassword(password);
            var user = _userRepo.GetUserByCredentials(username, encryptedPass).Result;

            return user == null ? 0 : user.Role;
        }
    }
}