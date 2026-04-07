using Microsoft.AspNetCore.Mvc;
using CopilotDemo.Models;

namespace CopilotDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Get user by ID with email and validation
        /// </summary>
        /// <param name="id">User ID (optional, defaults to 1)</param>
        /// <returns>User object with Id, Name, and Email</returns>
        [HttpGet]
        public IActionResult GetUser(int id = 1)
        {
            if (id <= 0)
            {
                return BadRequest("User ID must be greater than 0");
            }

            var user = new UserModel
            {
                Id = id,
                Name = "Amit",
                Email = "amit.thakur@example.com"
            };

            return Ok(user);
        }

        /// <summary>
        /// Validate user data
        /// </summary>
        /// <param name="user">User object to validate</param>
        /// <returns>Validation result</returns>
        [HttpPost("validate")]
        public IActionResult ValidateUser([FromBody] UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new { message = "User data is valid", user });
        }
    }
}