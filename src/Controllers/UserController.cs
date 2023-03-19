using Microsoft.AspNetCore.Mvc;
using Assignment1.Services;
using Assignment1.Models;
using System.Text;
using Assignment1.BusinessLogic;

namespace Assignment1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public IActionResult GetAllCashiers(int userId)
        {
            var user =_userService.GetUserById(userId);
            if(user == null ? false : user.Role == 2)
            {
                var users = _userService.GetAllCashiers();
                if(users == null) return BadRequest();
                return Ok(users);
            }
            return BadRequest("User not an admin!");
        }

        [HttpPost("{userId}")]
        public IActionResult CreateCashier([FromRoute] int userId, [FromBody] User cashier)
        {
            var user =_userService.GetUserById(userId);
            if(user == null ? false : user.Role == 2)
            {
                if(_userService.AddUser(cashier))
                {
                    return Created(nameof(GetCashierById), cashier);
                }
                else return BadRequest();
            }
            else return BadRequest("User not an admin!");
        }

        [HttpGet]
        [Route("get/{userId}")]
        private IActionResult GetCashierById([FromRoute] int userId, [FromBody] int gottenId)
        {
            var user =_userService.GetUserById(userId);
            if(user == null ? false : user.Role == 2)
            {
                var gottenUser = _userService.GetUserById(gottenId);
                if(gottenUser != null) return Ok(gottenUser);
                else return BadRequest();
            }
            else return BadRequest("User not an admin!");

        }

        [HttpPut("{userId}")]
        public IActionResult UpdateCashier([FromRoute] int userId, [FromBody] User cashier)
        {
            var user =_userService.GetUserById(userId);
            if(user == null ? false : user.Role == 2)
            {
                if(_userService.UpdateUser(cashier))
                    return Ok(cashier);
                else 
                    return BadRequest();
            }
            else return BadRequest("User not an admin!");
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteCashier([FromRoute] int userId, [FromBody] int deletedId)
        {
            var user =_userService.GetUserById(userId);
            if(user == null ? false : user.Role == 2)
            {
                var deleted = _userService.GetUserById(deletedId);
                if(deleted != null && _userService.DeleteUser(deleted))
                    return Ok(deleted);
                else
                    return BadRequest();
            }
            else return BadRequest("User not an admin!");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] UserDto credentials)
        {
            var userId = _userService.LogIn(credentials.Username, credentials.Password);
            if(userId > 0)
                return Ok(userId);
            else
                return NotFound();
        }
    }
}