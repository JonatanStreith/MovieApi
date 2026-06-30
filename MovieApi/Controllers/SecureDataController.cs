using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MovieApi.Controllers
{
    [Route("api/securedata")]
    [ApiController]
    [Authorize]
    public class SecureDataController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetSecureData()
        {
            var userName =
            User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            return Ok(new
            {
                Message = $"Grattis {userName}, du har nått en skyddad endpoint!" });
            }
    }
}
