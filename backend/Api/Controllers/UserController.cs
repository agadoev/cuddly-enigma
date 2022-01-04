using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Api.Dtos;
using Application.UseCases;
using Application.UseCases.RegisterUser;
using Application.UseCases.CreateWish;
using System.Net;

namespace Api.Controllers {
            
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase {

        private readonly IConfiguration _configuration;
        private readonly IUseCase<RegisterUserInput, RegisterUserOutput> _useCase;
        private readonly IUseCase<CreateWishInput, CreateWishOutput> _createWishUseCase;

        public UserController(
            IConfiguration configuration,
            IUseCase<RegisterUserInput, RegisterUserOutput> registerUserUseCase,
            IUseCase<CreateWishInput, CreateWishOutput> createWishUseCase
        ) { 
            _configuration = configuration;
            _useCase = registerUserUseCase;
            _createWishUseCase = createWishUseCase;
        }

        [HttpGet]
        public void GetAll() {
        }

        [HttpPost]
        public IActionResult Register([FromBody]UserDto dto) {

            var input = new RegisterUserInput() {
                Name = dto.Name
            };

            var output = _useCase.Execute(input);

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
