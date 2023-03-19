using Assignment1.Models;
using Assignment1.Repositories;
using Assignment1.Repositories.Interfaces;

namespace Assignment1.BusinessLogic
{
    public class ShowService
    {
        private readonly IArtistRepository _artistRepo;
        private readonly IShowRepository _showRepo;
        private readonly ITicketRepository _ticketRepo;

        public ShowService(IArtistRepository artistRepo,
                                    IShowRepository showRepo,
                                    ITicketRepository ticketRepo)
        {
            _ticketRepo = ticketRepo;
            _showRepo = showRepo;
            _artistRepo = artistRepo;
        }

        public List<Show> GetShows()
        {
            var shows = _showRepo.GetAll().Result;
            return shows.ToList();
        }

        public List<Show> GetShowByArtistId(int artistId)
        {
            var shows = _showRepo.GetAll().Result.Where(s => s.ArtistId == artistId);
            return shows.ToList();
        }

        public bool UpdateShow(Show show)
        {
            return _showRepo.Update(show).Result;
        }

        public bool AddShow(Show show)
        {
            return _showRepo.Add(show).Result;
        }

        public bool DeleteShow(int showId)
        {
            var show = _showRepo.GetById(showId).Result;
            if (show == null) return false;
            else
            {
                var tickets = _ticketRepo.GetTicketsByShow(showId).Result;
                foreach(Ticket t in tickets)
                {
                    _ticketRepo.Delete(t);
                }
                return _showRepo.Delete(show).Result;
            }
        }

        public Show GetShowById(int showId)
        {
            return _showRepo.GetById(showId).Result;
        }
    }
}