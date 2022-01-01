using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Core.UseCases;
using Core;

namespace Api.Controllers {
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
        public IEnumerable<object> Register() {
            Console.WriteLine(_usersStorage);
            return new object[] {};
        }
    }
}
