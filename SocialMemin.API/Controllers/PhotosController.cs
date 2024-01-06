using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMemin.Application.Core;
using SocialMemin.Application.Photos;

namespace SocialMemin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
    
        private IMediator _mediator;

        public PhotosController(IMediator mediator) => _mediator = mediator;


        [HttpPost]
        public async Task<IActionResult> Add([FromForm] Add.Command command) => HandleResult(await _mediator.Send(command));
        

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) => HandleResult(await _mediator.Send(new Delete.Command { Id = id }));
        

        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMain(string id) => HandleResult(await _mediator.Send(new SetMain.Command { Id = id }));


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
