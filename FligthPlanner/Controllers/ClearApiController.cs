using FligthPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FligthPlanner.Controllers
{
    [ApiController]
    [Route("testing-api")]
    public class ClearApiController : ControllerBase
    {
        private readonly ICleanupService _service;

        public ClearApiController(ICleanupService service) => _service = service;

        [HttpPost("clear")]
        public IActionResult Clear(int id)
        {
            _service.Cleaner();

            return Ok();
        }
    }
}
