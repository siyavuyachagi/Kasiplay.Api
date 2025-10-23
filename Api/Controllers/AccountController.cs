using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    [Authorize]
    public class AccountController : Controller
    {
        public AccountController() { }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost("register")]
        [AutoValidateAntiforgeryToken]
        [AllowAnonymous]
        public IActionResult Register([FromBody] object profileData)
        {
            try
            {
                // Update profile logic here
                return Ok(new { message = "Profile updated successfully." });
            }
            catch
            {
                return BadRequest(new { message = "Failed to update profile." });
            }
        }


        [HttpPost("profile")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ProfileInfo()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
