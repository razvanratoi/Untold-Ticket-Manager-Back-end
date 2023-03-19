using Assignment1.Data;
using Assignment1.Models;
using Assignment1.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assignment1.Repositories
{
    public class ShowRepository : GenericRepository<Show>, IShowRepository
    {
        private readonly DataContext _context;
        public ShowRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Show?> GetShowByTitle(string title)
        {
            return await _context.Shows.FirstOrDefaultAsync(s => s.Title == title);
        }

        
    }
}