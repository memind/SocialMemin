using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMemin.Application.Core;
using SocialMemin.Application.Profiles;

namespace SocialMemin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private IMediator _mediator;

        public ProfilesController(IMediator mediator) => _mediator = mediator;
        

        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfile(string username) => HandleResult(await _mediator.Send(new Details.Query { Username = username }));
        

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null) return NotFound();

            if (result.IsSuccess && result.Value != null)
                return Ok(result.Value);

            if (result.IsSuccess && result.Value == null)
                return NotFound();

            return BadRequest(result.Error);
        }
    }
}
