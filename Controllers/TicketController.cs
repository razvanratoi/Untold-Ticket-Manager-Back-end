using Assignment1.BusinessLogic;
using Assignment1.Models;
using Assignment1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly TicketService _ticketService;
        private readonly UserService _userService;
        public TicketController(TicketService ticketService, UserService userService)
        {
            _userService = userService;
            _ticketService = ticketService;
        }

        [HttpGet("{userId}")]
        public IActionResult GetTicketsByShow([FromRoute] int userId, [FromBody] int showId)
        {
            var user = _userService.GetUserById(userId);

            if(user != null)
            {
                if(user.Role == 1)
                {
                    var tickets = _ticketService.GetTicketsByShow(showId);
                    if(tickets != null)
                        return Ok(tickets);
                    else
                        return Ok(null);
                }
                else
                {
                    _ticketService.CreateFile(showId);
                    return Ok(null);
                }
            }
            else return NotFound("Cashier not found");
        }

        [HttpDelete("{userId}")]
        public IActionResult CancelReservation([FromRoute] int userId, [FromBody] int ticketId)
        {
            var user = _userService.GetUserById(userId);

            if(user == null ? false : user.Role == 1)
            {
                var ticket = _ticketService.GetticketById(ticketId);
                if(ticket != null && _ticketService.DeleteTicket(ticket).Result)
                    return Ok(ticket);
                else
                    return BadRequest();
            }
            else return NotFound("Cashier not found");
        }

        [HttpPut("{userId}")]
        public IActionResult EditReservation([FromRoute] int userId, [FromBody] Ticket ticket)
        {
            var user = _userService.GetUserById(userId);

            if(user == null ? false : user.Role == 1)
            {
                if( _ticketService.UpdateTicket(ticket).Result)
                    return Ok(ticket);
                else
                    return BadRequest();
            }
            else return NotFound("Cashier not found");
        }

        [HttpPost("{userId}")]
        public IActionResult SellTicket([FromRoute] int userId, [FromBody] TicketDto ticketDto)
        {
            var user = _userService.GetUserById(userId);

            if(user == null ? false : user.Role == 1)
            {
                if(_ticketService.AddTicket(ticketDto.ShowTitle, ticketDto.Places, userId).Result)
                    return Created(nameof(GetTicketsByShow), ticketDto);
                else
                    return BadRequest("Exceeded number of available tickets");
            }
            else return NotFound("Cashier not found");
        }


    }
}