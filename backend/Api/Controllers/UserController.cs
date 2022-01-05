using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Api.Dtos;
using Application.UseCases;
using Application.UseCases.CreateWish;
using System.Net;

namespace Api.Controllers {
            
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase {

        private readonly IConfiguration _configuration;
        private readonly ICommandHandler<RegisterUserCommand> _registerUserHandler;
        private readonly IUseCase<CreateWishInput, CreateWishOutput> _createWishUseCase;

        public UserController(
            IConfiguration configuration,
            ICommandHandler<RegisterUserCommand> registerUserHandler,
            IUseCase<CreateWishInput, CreateWishOutput> createWishUseCase
        ) { 
            _configuration = configuration;
            _registerUserHandler = registerUserHandler;
            _createWishUseCase = createWishUseCase;
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
            var input = new CreateWishInput();

            // мапим DTO в Input
            input.UserId = dto.UserId;
            input.WishTitle = dto.WishDto.Title;

            var output = _createWishUseCase.Execute(input);

            return output.Success ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}
