using Microsoft.AspNetCore.Mvc;

namespace CopilotDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok(new { Id = 1, Name = "Amit" });
        }
    }
}