using System;
using Microsoft.AspNetCore.Mvc;
using Api.Dtos;
using Application.UseCases;

namespace Api.Controllers {

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WishesController : ControllerBase {

        private readonly ICommandHandler<ReserveWishCommand> _reserveWishHandler;
        private readonly ICommandHandler<CreateWishCommand> _createWishHandler;
        private readonly ICommandHandler<RemoveWishCommand> _removeWishHandler;

        public WishesController(
            ICommandHandler<ReserveWishCommand> reserveWishHandler,
            ICommandHandler<CreateWishCommand> createWishHandler,
            ICommandHandler<RemoveWishCommand> removeWishHandler
        ) {
            _reserveWishHandler = reserveWishHandler;
            _createWishHandler = createWishHandler;
            _removeWishHandler = removeWishHandler;
        }


        [HttpPost]
        public IActionResult Create([FromBody]CreateWishDto dto) {
            try {
                var command = new CreateWishCommand();

                command.UserId = dto.UserId;
                command.WishTitle = dto.Title;
                command.WishUrl = dto.Url;
            
                _createWishHandler.Execute(command);

                return Ok();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
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
        public IActionResult Delete([FromQuery]string wishId, [FromQuery]string userId) {
            try {
                var command = new RemoveWishCommand();

                command.WishId = new Guid(wishId);
                command.UserId = new Guid(userId);

                _removeWishHandler.Execute(command);

                return Ok();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
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