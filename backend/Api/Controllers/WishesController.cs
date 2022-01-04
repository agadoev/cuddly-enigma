using Microsoft.AspNetCore.Mvc;
using Api.Dtos;
using Application.UseCases;

namespace Api.Controllers {

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WishesController : ControllerBase {

        private readonly ICommandHandler<ReserveWishCommand> _reserveWishHandler;

        public WishesController(
            ICommandHandler<ReserveWishCommand> reserveWishHandler
        ) {
            _reserveWishHandler = reserveWishHandler;
        }


        [HttpPost]
        public IActionResult Create([FromBody]WishDto dto) {
            return Ok();
        }

        [HttpGet]
        public WishDto[] GetUserWishList([FromQuery]string userId) {
            return new WishDto[] {};
        }

        [HttpPut]
        public IActionResult Update([FromQuery]string wishId) {
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery]string wishId) {
            return Ok();
        }

        [HttpPost]
        public IActionResult Reserve([FromBody]ReserveWishDto dto) {

            var command = new ReserveWishCommand();

            command.ReserverId = dto.ReservedId;
            command.WishId = dto.WishId;    

            command = _reserveWishHandler.Execute(command);
            return Ok();
        }

        [HttpPost]
        public IActionResult DiscardReservation([FromBody]DiscardReservationDto dto) {
            return Ok();
        }
    }
}