using AuthService.Application.Features.ExternalApplication.Get;
using AuthService.Web.Constants;
using AuthService.Web.ViewModels.Auth;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Web.Controllers
{
    [Route("/auth")]
    [ApiController]
    public class AuthController(IGetApplicationCredentials credentialsService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> RedirectToAuthPage([FromForm] AuthRequest request)
        {
            (Guid clientId, string clientSecret, string redirectUrl) = request;

            if (await credentialsService.IsValid(clientId, clientSecret))
            {
                HttpContext.Session.SetString(SessionNames.ClientId, clientId.ToString());
                HttpContext.Session.SetString(SessionNames.ClientSecret, clientSecret);
                HttpContext.Session.SetString(SessionNames.RedirectUrl, redirectUrl);

                return RedirectPermanent("/login");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
