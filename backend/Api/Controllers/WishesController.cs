using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Api.Dtos;
using Application.UseCases;
using Api.Mappers;
using Domain;

namespace Api.Controllers {

    [ApiController]
    public class WishesController : ControllerBase {

        private readonly ICommandHandler<GetWishesByUserCommand> _getWishesByUser;
        private readonly ICommandHandler<CreateWishCommand> _createWish;
        private readonly ICommandHandler<RemoveWishCommand> _removeWish;
        private IMapperBase<Wish, WishDto> _mapper;

        public WishesController(
            ICommandHandler<CreateWishCommand> createWishHandler,
            ICommandHandler<RemoveWishCommand> removeWishHandler,
            ICommandHandler<GetWishesByUserCommand> getWishesByUser
        ) {
            _createWish = createWishHandler;
            _removeWish = removeWishHandler;
            _getWishesByUser = getWishesByUser;
            _mapper = new WishMapper();
        }

        [HttpPost("/wishes/")]
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

        [HttpGet("/wishes/{userId}")]
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

        [HttpPut("/wishes/{wishId}")]
        public IActionResult Update([FromQuery]string wishId) {
            return Ok();
        }

        [HttpDelete("/wishes/{wishId}/{userId}")]
        public IEnumerable<WishDto> Delete([FromQuery]string wishId, [FromQuery]string userId) {
            var command = new RemoveWishCommand();

            command.WishId = new Guid(wishId);
            command.UserId = new Guid(userId);

            _removeWish.Execute(command);

            return command.newWishes.Select((w) => _mapper.Map(w));
        }
    }
}