using Microsoft.AspNetCore.Mvc;
using Api.Dtos;

namespace Api.Controllers {

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WishesController : ControllerBase {

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
        public IActionResult Reserve([FromBody]ReserveDto dto) {
            return Ok();
        }

        [HttpPost]
        public IActionResult DiscardReservation([FromBody]DiscardReservationDto dto) {
            return Ok();
        }
    }
}