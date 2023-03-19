using Assignment1.Models;

namespace Assignment1.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> UserExists(string username, string pass);
        Task<User?> GetUserByEmail(string username);
        Task<User?> GetUserByCredentials(string username, string password);
    }
}