﻿using MediatR;
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

        [HttpPut]
        public async Task<IActionResult> Edit(Edit.Command command) => HandleResult(await _mediator.Send(command));

        [HttpGet("{username}/activities")]
        public async Task<IActionResult> GetUserActivities(string username, string predicate) => HandleResult(await _mediator.Send(new ListActivities.Query { Username = username, Predicate = predicate }));
        
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
