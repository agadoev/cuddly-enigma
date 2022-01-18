using Microsoft.AspNetCore.Mvc;

namespace Api {
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HealthCheckController : ControllerBase {

        [HttpGet]
        public IActionResult Check() {
            return StatusCode(200);
        }
    }
}