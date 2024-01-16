using Application.CQRS.Commands.FormActions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoogleFormsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserFormController : Controller
    {
        private readonly IMediator _mediator;

        public UserFormController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserForm([FromRoute] Guid id)
        {
            var deleteResult = await _mediator.Send(new Delete.Command { Id = id });
            if (!deleteResult.Success)
            {
                return BadRequest(deleteResult.Message);
            }

            return Ok();
        }
    }
}
