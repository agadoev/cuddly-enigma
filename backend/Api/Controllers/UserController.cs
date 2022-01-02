using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Core.UseCases;
using Api.Dtos;
using Core;
using Data;

namespace Api.Controllers {
            
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase {

        private readonly IDbContext<User> _usersStorage;
        private readonly IConfiguration _configuration;
        private readonly SqliteContext _context;

        public UserController(
            IDbContext<User> usersStorage,
            IConfiguration configuration
        ) {
            _usersStorage = usersStorage;
            _configuration = configuration;
            _context = new SqliteContext(); 
        }

        [HttpGet]
        public void GetAll() {
        }

        [HttpPost]
        public IActionResult Register([FromBody]UserDto dto) {

            return Ok();
        }
    }
}
