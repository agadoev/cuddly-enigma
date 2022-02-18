using System;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {


    [ApiController]
    public class EventsController : ControllerBase {

        [HttpPost("/events/")]
        public IActionResult Create() {
            throw new NotImplementedException();
        }

        [HttpGet("/events/{id}")]
        public IActionResult Get([FromQuery]string id) {
            throw new NotImplementedException();
        }

        [HttpGet("/events/all/")]
        public IActionResult GetAll () {
            throw new NotImplementedException();
        }

        [HttpPut("/events/{id}")]
        public IActionResult Put([FromQuery]string id) {
            throw new NotImplementedException();
        }

        [HttpDelete("/events/{id}")]
        public IActionResult Delete([FromQuery]string id) {
            throw new NotImplementedException();
        }

    }
}