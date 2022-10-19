using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApplication.Models;
using NotesApplication.Service;

namespace NotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(AppDbContext context)
        {
            _userService = new UserService(context);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _userService.Create(user);

            return CreatedAtAction("GetUser", new { Id = user.Id }, user);
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_userService.GetAll().ToList());
        }

        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _userService.Update(id, user);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }
    }
}

