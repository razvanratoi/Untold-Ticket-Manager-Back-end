using System.ComponentModel.DataAnnotations;

namespace Assignment1.Models
{
    public class Ticket
    {
        public Ticket(int cashierId, int showId, int places)
        {
            CashierId = cashierId;
            ShowId = showId;
            Places = places;
        }

        [Key]
        public int Id { get; set; }
        public int CashierId { get; set; }
        public int ShowId { get; set; }
        public int Places { get; set; }

        
        }
}