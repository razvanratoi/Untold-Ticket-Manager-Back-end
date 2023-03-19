using Assignment1.Models;
using Assignment1.Repositories.Interfaces;
using Assignment1.Services;
using Assignment1.Services.AbstractServices;

namespace Assignment1.BusinessLogic
{
    public class TicketService
    {
        private readonly ITicketRepository _ticketRepo;
        private readonly IArtistRepository _artistRepo;
        private readonly IShowRepository _showRepo;
        public TicketService(ITicketRepository ticketRepo,
                            IArtistRepository artistRepo,
                            IShowRepository showRepo)
        {
            _artistRepo = artistRepo;
            _ticketRepo = ticketRepo;
            _showRepo = showRepo;
        }

        public Ticket? GetticketById(int id)
        {

            return _ticketRepo.GetById(id).Result;
        }

        public List<Ticket> GetAll()
        {
            return _ticketRepo.GetAll().Result.ToList();
        }

        public async Task<bool> AddTicket(string showTitle, int places, int cashierId)
        {
            var show = _showRepo.GetShowByTitle(showTitle).Result;

            if (show == null) return false;

            if (show.MaxNbTickets < places) return false;


            Ticket ticket = new Ticket(cashierId, show.Id, places);
            await _showRepo.Update(show.ReserveTickets(places));
            return _ticketRepo.Add(ticket).Result;
        }

        public async Task<bool> UpdateTicket(Ticket ticket)
        {
            var original = _ticketRepo.GetById(ticket.Id).Result;
            if(original != null)
            {
                if(original.ShowId != ticket.ShowId)
                {
                    var show =  _showRepo.GetById(original.ShowId).Result;
                    await _showRepo.Update(show.ReserveTickets(-original.Places));
                    show =  _showRepo.GetById(ticket.ShowId).Result;
                    await _showRepo.Update(show.ReserveTickets(ticket.Places));
                }
                else
                {
                    var show = _showRepo.GetById(ticket.ShowId).Result;
                    await _showRepo.Update(show.ReserveTickets(-original.Places));
                    await _showRepo.Update(show.ReserveTickets(ticket.Places));
                }
            }
            return _ticketRepo.Update(ticket).Result;
        }

        public async Task<bool> DeleteTicket(Ticket ticket)
        {
            var show = await _showRepo.GetById(ticket.ShowId);
            await _showRepo.Update(show.ReserveTickets(-ticket.Places));
            return _ticketRepo.Delete(ticket).Result;
        }

        public List<Ticket> GetTicketsByShow(int showId)
        {
            return _ticketRepo.GetTicketsByShow(showId).Result.ToList();
        }

        public void CreateFile(int showId)
        {
            var show = _showRepo.GetById(showId).Result;
            if (show == null) return;
            var artist = _artistRepo.GetById(show.ArtistId).Result;
            if (artist == null) return;
            var tickets = _ticketRepo.GetTicketsByShow(showId).Result.ToList();

            if (show != null && tickets.Count != 0)
            {
                AbstractCreator xmlCreator = new XmlCreator();
                xmlCreator.createFile(tickets, show, artist);
            }
            else return;
        }
    }
}