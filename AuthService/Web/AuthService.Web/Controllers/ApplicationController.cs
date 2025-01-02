using AuthService.Application.Features.ExternalApplication.Register;
using AuthService.Application.ResultBase;
using AuthService.Web.ViewModels.ApplicationInteraction;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController(IApplicationRegister registerService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RegisterNewApplication([FromQuery] string name)
        {
            Result<(Guid id, string token)> result = await registerService.RegisterApplication(name);

            NewApplicationCredentials response = new(result.Data.id, result.Data.token);

            return Ok(response);
        }
    }
}
