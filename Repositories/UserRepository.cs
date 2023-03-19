using Assignment1.Data;
using Assignment1.Models;
using Assignment1.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assignment1.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context) : base(context)
        {
            _context = context;

        }
        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task<User?> GetUserByEmail(string username, string pass)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UserExists(string username, string pass)
        {
            return await _context.Users.AnyAsync(u => u.Username == username && u.Password == pass);
        }

        public async Task<User?> GetUserByCredentials(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && 
                                                    u.Password == password);
        }
    }
}