using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AccountController : Controller
    {
        public AccountController() { }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
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
    }
}
