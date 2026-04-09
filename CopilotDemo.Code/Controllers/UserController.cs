using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CopilotDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private const string EmailRegexPattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";

        [HttpGet]
        public IActionResult GetUser()
        {
            string email = "amit@example.com";

            if (!ValidateEmail(email))
            {
                return BadRequest(new { error = "Invalid email format" });
            }

            return Ok(new { Id = 1, Name = "Amit", Email = email });
        }

        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                return Regex.IsMatch(email, EmailRegexPattern, RegexOptions.IgnoreCase);
            }
            catch
            {
                return false;
            }
        }
    }
}