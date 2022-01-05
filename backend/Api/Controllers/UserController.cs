using System;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Api.Dtos;
using Application.UseCases;
using System.Net;

namespace Api.Controllers {
            
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase {

        private readonly IConfiguration _configuration;
        private readonly ICommandHandler<RegisterUserCommand> _registerUserHandler;
        private readonly ICommandHandler<CreateWishCommand> _createWishHandler;

        public UserController(
            IConfiguration configuration,
            ICommandHandler<RegisterUserCommand> registerUserHandler,
            ICommandHandler<CreateWishCommand> createWishHandler
        ) { 
            _configuration = configuration;
            _registerUserHandler = registerUserHandler;
            _createWishHandler = createWishHandler;
        }

        [HttpGet]
        public void GetAll() {

        }

        [HttpPost]
        public JsonResult Register([FromBody]UserDto dto) {

            try {
                var command = new RegisterUserCommand() {
                    Username = dto.Name
                };

                _registerUserHandler.Execute(command);

                return new JsonResult(new { id = command.Id});
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return null;
            }

        }
    }
}
