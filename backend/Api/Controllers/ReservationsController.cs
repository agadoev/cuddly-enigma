using Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases;

namespace Api.Controllers {

    [ApiController]
    [Route("[controller]/[action]")]
    class ReservationController : ControllerBase {

        private readonly ICommandHandler<ReserveWishCommand> _reserveWish;

        public ReservationController(
            ICommandHandler<ReserveWishCommand> reserveWishHandler
        ){
            _reserveWish = reserveWishHandler; 
        }

        [HttpPost]
        public IActionResult Add([FromBody] ReserveWishDto dto) {
            var command = new ReserveWishCommand();

            command.ReserverId = dto.ReservedId;
            command.WishId = dto.WishId;    

            _reserveWish.Execute(command);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery]string reservationId) {

            return Ok();    
        }
    }
}