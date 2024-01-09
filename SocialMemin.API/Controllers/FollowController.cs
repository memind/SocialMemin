using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMemin.Application.Core;
using SocialMemin.Application.Followers;

namespace SocialMemin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FollowController(IMediator mediator) => _mediator = mediator;
        

        [HttpPost("{username}")]
        public async Task<IActionResult> Follow(string username) => HandleResult(await _mediator.Send(new FollowToggle.Command { TargetUsername = username }));
        

        [HttpGet("{username}")]
        public async Task<IActionResult> GetFollowings(string username, string predicate) => HandleResult(await _mediator.Send(new List.Query { Username = username, Predicate = predicate }));
        

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
