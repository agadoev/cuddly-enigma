using Microsoft.AspNetCore.Mvc;

namespace Api {
    [ApiController]
    [Route("[controller]/[action]")]
    public class HealthCheckController : ControllerBase {

        [HttpGet]
        [Route("/healthcheck/")]
        public IActionResult Index() {
            return StatusCode(200);
        }
    }
}