using System.ComponentModel.DataAnnotations;

namespace Assignment1.Models
{
    public class Show
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime DateAndTime { get; set; }
        public int ArtistId { get; set; }
        public int MaxNbTickets { get; set; }

        public Show ReserveTickets(int tickets)
        {
            this.MaxNbTickets -= tickets;
            return this;
        }
    }
}