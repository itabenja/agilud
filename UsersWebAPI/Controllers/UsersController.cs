using Microsoft.AspNetCore.Mvc;
using UsersWebAPI.Models;
using UsersWebAPI.Repositories;

namespace UsersWebAPI.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UsersController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_repo.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _repo.GetById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            var created = _repo.Create(user);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
    }
}
