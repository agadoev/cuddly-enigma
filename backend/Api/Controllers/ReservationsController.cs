using Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases;

namespace Api.Controllers {

    [ApiController]
    public class ReservationController : ControllerBase {

        private readonly ICommandHandler<ReserveWishCommand> _reserveWish;

        public ReservationController(
            ICommandHandler<ReserveWishCommand> reserveWishHandler
        ){
            _reserveWish = reserveWishHandler; 
        }

        [HttpPost("/reservations/")]
        public IActionResult Add([FromBody] ReserveWishDto dto) {
            var command = new ReserveWishCommand();

            command.ReserverId = dto.ReservedId;
            command.WishId = dto.WishId;    

            _reserveWish.Execute(command);
            return Ok();
        }

        [HttpDelete("/reservations/{id}")]
        public IActionResult Delete([FromQuery]string id) {

            return Ok();    
        }
    }
}