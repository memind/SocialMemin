using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMemin.Application.Activities;
using SocialMemin.Application.Core;
using SocialMemin.Domain;
using SocialMemin.Persistence;

namespace SocialMemin.API.Controllers
{
    [Route("/api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private IMediator _mediator;

        public ActivitiesController( IMediator mediator) => _mediator = mediator;


        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities() => HandleResult(await _mediator.Send(new List.Query()));


        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity(Guid id) => HandleResult(await _mediator.Send(new Details.Query { Id = id }));


        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity activity) => HandleResult(await _mediator.Send(new Create.Command { Activity = activity }));


        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, Activity activity) => HandleResult(await _mediator.Send(new Edit.Command { Activity = activity }));


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) => HandleResult(await _mediator.Send(new Delete.Command { Id = id }));

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
