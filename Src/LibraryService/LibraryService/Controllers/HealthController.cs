using LibraryService.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Controllers
{
    [ApiController]
    [Route("api/health")]
    public class HealthController : ControllerBase
    {

        [HttpGet()]
        public async Task<IActionResult> GetHealth()
        {
            return Ok("API Service running...");
        }

    }
}
