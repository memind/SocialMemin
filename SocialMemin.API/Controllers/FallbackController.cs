using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SocialMemin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class FallbackController : ControllerBase
    {
        public IActionResult Index() => PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","index.html"), "text/HTML");
        
    }
}
