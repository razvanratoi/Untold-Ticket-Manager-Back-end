using Assignment1.Models;
using Assignment1.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShowController : ControllerBase
    {
        private readonly ShowService _showService;
        private readonly UserService _userService;
        public ShowController(ShowService showService, UserService userService)
        {
            _userService = userService;
            _showService = showService;

        }

        [HttpGet("{userId}")]
        public IActionResult GetAllShows([FromRoute] int userId, [FromBody] string diff)
        {
            var user = _userService.GetUserById(userId);
            if(user == null ? false : user.Role == 2)
            {
                var shows = _showService.GetShows();
                if(shows != null) return Ok(shows);
                else return BadRequest();
            }
            return BadRequest("User not an admin");
        }

        [HttpGet("artist/{userId}")]
        public IActionResult GetShowsByArtist([FromRoute] int userId, [FromBody] int artistId)
        {
            var user = _userService.GetUserById(userId);
            if(user == null ? false : user.Role == 2)
            {
                var shows = _showService.GetShowByArtistId(artistId);
                if(shows != null) return Ok(shows);
                else return BadRequest();
            }
            return BadRequest("User not an admin");
        }

        [HttpPut("{userId}")]
        public IActionResult UpdateShow([FromRoute] int userId, [FromBody] Show show)
        {
            var user = _userService.GetUserById(userId);
            if(user == null ? false : user.Role == 2)
            {
                return Ok(_showService.UpdateShow(show));
            }
            return BadRequest("User not an admin");
        }

        [HttpPost("{userId}")]
        public IActionResult AddShow([FromRoute] int userId, [FromBody] Show show)
        {
            var user = _userService.GetUserById(userId);
            if(user == null ? false : user.Role == 2)
            {
                return Created(nameof(GetAllShows), _showService.AddShow(show));
            }
            return BadRequest("User not an admin");
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteShow([FromRoute] int userId, [FromBody] int showId)
        {
            var user = _userService.GetUserById(userId);
            if(user == null ? false : user.Role == 2)
            {
                return Ok(_showService.DeleteShow(showId));
            }
            return BadRequest("User not an admin");
        }
    }
}