using Assignment1.BusinessLogic;
using Assignment1.Models;
using Assignment1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Tests
{
    [ApiController]
    [Route("test")]
    public class Tester : ControllerBase
    {
        private const string ENCRYPTED = """UGFyb2xhMTIzNA==""";
        private readonly ShowService _showService;
        private readonly TicketService _ticketService;
        public Tester(ShowService showService, TicketService ticketService)
        {
            _ticketService = ticketService;
            _showService = showService;

        }

        [HttpPost]
        public IActionResult testPasswordEncryption()
        {
            var encrypted = ENCRYPTED;
            var password = PasswordService.encryptPassword("Parola1234");

            if (password == encrypted)
                return Ok("Password encryption works");
            else
                return StatusCode(500);
        }

        [HttpGet]
        public IActionResult testSeatsExceeded()
        {
            var show = _showService.GetShowById(2);
            var available = show.MaxNbTickets + 1;
            
            if(_ticketService.AddTicket(show.Title, available, 3).Result == false)
                return Ok("test works");
            else
                return StatusCode(500);
        }
    }
}