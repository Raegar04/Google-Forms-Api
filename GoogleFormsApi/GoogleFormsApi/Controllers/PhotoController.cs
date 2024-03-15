using Application.CQRS.Commands.PhotoActions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoogleFormsApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PhotoController : Controller
    {
        private readonly IMediator _mediatr;

        public PhotoController(IMediator mediator)
        {
            _mediatr = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm]IFormFile File, [FromQuery]bool isMain) 
        {
            await _mediatr.Send(new Upload.Command() 
            {
                File = File,
                IsMain = isMain
            });

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]string id) 
        {
            await _mediatr.Send(new Delete.Command() { Id = id });

            return Ok();
        }
    }
}
