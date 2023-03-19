using Assignment1.Data;
using Assignment1.Models;
using Assignment1.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assignment1.Repositories
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        private readonly DataContext _context;
        public TicketRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByShow(int showId)
        {
            return await _context.Tickets.Where(t => t.ShowId == showId).ToListAsync();
        }
    }
}