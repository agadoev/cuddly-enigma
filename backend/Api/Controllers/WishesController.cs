using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Api.Dtos;
using Application.UseCases;

namespace Api.Controllers {

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WishesController : ControllerBase {


        // TODO: Сделать нормальный класс базового контроллера, прикрутить к нему логгер

        private readonly ICommandHandler<GetWishesByUserCommand> _getWishesByUser;
        private readonly ICommandHandler<ReserveWishCommand> _reserveWish;
        private readonly ICommandHandler<CreateWishCommand> _createWish;
        private readonly ICommandHandler<RemoveWishCommand> _removeWish;

        public WishesController(
            ICommandHandler<ReserveWishCommand> reserveWishHandler,
            ICommandHandler<CreateWishCommand> createWishHandler,
            ICommandHandler<RemoveWishCommand> removeWishHandler,
            ICommandHandler<GetWishesByUserCommand> getWishesByUser
        ) {
            _reserveWish = reserveWishHandler;
            _createWish = createWishHandler;
            _removeWish = removeWishHandler;
            _getWishesByUser = getWishesByUser;
        }



        /// <summary>
        ///
        /// </summary>
        [HttpPost]
        public IActionResult Create([FromBody]CreateWishDto dto) {
            try {
                var command = new CreateWishCommand();

                command.UserId = dto.UserId;
                command.WishTitle = dto.Title;
                command.WishUrl = dto.Url;
            
                _createWish.Execute(command);

                return Ok();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        ///     <returns>Список всех Wish конкретного пользователя</returns>
        /// </summary>
        [HttpGet]
        public WishDto[] GetUserWishList([FromQuery]string userId) {

            try {
                var command = new GetWishesByUserCommand();

                command.UserId = new Guid(userId);

                _getWishesByUser.Execute(command);

                var dtos = command.Wishes.Select(w => new WishDto { Title = w.Title, Url = w.Url, WishId = w.Id, UserId = w.UserId }).ToArray();
                
                return dtos;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpPut]
        public IActionResult Update([FromQuery]string wishId) {
            return Ok();
        }


        /// <summary>
        ///     <returns>Обновленный список всех Wish</returns>
        /// </summary>
        [HttpDelete]
        public IEnumerable<WishDto> Delete([FromQuery]string wishId, [FromQuery]string userId) {
            var command = new RemoveWishCommand();

            command.WishId = new Guid(wishId);
            command.UserId = new Guid(userId);

            _removeWish.Execute(command);

            var wishDtos = command.newWishes.Select((w) => new WishDto() {
                WishId = w.Id,
                UserId = w.UserId,
                Title = w.Title,
                Url = w.Url
            });

            return wishDtos;
        }

        [HttpPost]
        public IActionResult Reserve([FromBody]ReserveWishDto dto) {

            var command = new ReserveWishCommand();

            command.ReserverId = dto.ReservedId;
            command.WishId = dto.WishId;    

            _reserveWish.Execute(command);
            return Ok();
        }

        [HttpPost]
        public IActionResult DiscardReservation([FromBody]DiscardReservationDto dto) {
            return Ok();
        }
    }
}