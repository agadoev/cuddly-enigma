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
        public IActionResult Register([FromBody]UserDto dto) {

            var command = new RegisterUserCommand() {
                Username = dto.Name
            };

            var output = _registerUserHandler.Execute(command);

            return output.Success ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpPost]
        public IActionResult CreateWish([FromBody]CreateWishDto dto) {
            var command = new CreateWishCommand();

            // мапим DTO в Input
            command.UserId = dto.UserId;
            command.WishTitle = dto.WishDto.Title;

            _createWishHandler.Execute(command);

            return command.Success ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}
