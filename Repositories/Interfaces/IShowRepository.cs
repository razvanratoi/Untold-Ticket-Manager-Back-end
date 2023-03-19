using Assignment1.Models;

namespace Assignment1.Repositories.Interfaces
{
    public interface IShowRepository : IGenericRepository<Show>
    {
         Task<Show?> GetShowByTitle(string title);
    }
}