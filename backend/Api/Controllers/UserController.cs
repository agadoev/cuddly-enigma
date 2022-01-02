using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Core.UseCases;
using Core;

namespace Api.Controllers {
            
    public class UserDto {
        public string Name {get; set;}
    }

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase {

        private readonly IDbContext<User> _usersStorage;

        public UserController(
            IDbContext<User> usersStorage
        ) {
            _usersStorage = usersStorage;
        }

        [HttpPost]
        public IActionResult Register([FromBody]UserDto dto) {
            Console.WriteLine(dto.Name);
            return Ok();
        }
    }
}
