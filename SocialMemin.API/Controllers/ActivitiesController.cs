using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMemin.Application.Activities;
using SocialMemin.Domain;
using SocialMemin.Persistence;

namespace SocialMemin.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private IMediator _mediator;

        public ActivitiesController( IMediator mediator) => _mediator = mediator;
        

        [HttpGet]
        public async Task<List<Activity>> GetActivities() => await _mediator.Send(new List.Query());
        

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity(Guid id) => await _mediator.Send(new Details.Query { Id = id });
        

        [HttpPost]
        public async Task CreateActivity(Activity activity) => await _mediator.Send(new Create.ActivityRequest() { Activity = activity});
        

        [HttpPut("{id}")]
        public async Task Edit(Guid id, Activity activity) => await _mediator.Send(new Edit.Command() { Activity = activity, Id = id });


        [HttpDelete("{id}")]
        public async Task Delete(Guid id) => await _mediator.Send(new Delete.Command() { Id = id });
    }
}
