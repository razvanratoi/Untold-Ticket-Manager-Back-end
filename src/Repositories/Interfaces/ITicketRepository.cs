using Assignment1.Models;

namespace Assignment1.Repositories.Interfaces
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
         Task<IEnumerable<Ticket>> GetTicketsByShow(int showId);
    }
}