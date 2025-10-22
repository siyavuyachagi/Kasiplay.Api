using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }


        public class LoginModel
        {
            [Required]
            public string Username { get; set; }
            [Required]
            public string Password { get; set; }
            public bool RememberMe { get; set; } = false;
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login([FromBody] LoginModel loginModel)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        [HttpPost("refresh")]
        [ValidateAntiForgeryToken]
        public IActionResult Refresh([FromBody] string refreshToken)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        [HttpPost("verify")]
        [ValidateAntiForgeryToken]
        public IActionResult Verify([FromBody] string accessToken)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public IActionResult Logout([FromBody] string accessToken)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
