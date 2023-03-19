using Assignment1.Data;
using Assignment1.Models;
using Assignment1.Repositories.Interfaces;

namespace Assignment1.Repositories
{
    public class ArtistRepository : GenericRepository<Artist>, IArtistRepository
    {
        public ArtistRepository(DataContext context) : base(context)
        {
        }
    }
}