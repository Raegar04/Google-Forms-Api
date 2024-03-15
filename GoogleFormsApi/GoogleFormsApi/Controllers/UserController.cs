using Application.CQRS.Queries.UserActions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoogleFormsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new Details.Query() { Id = id });

            return Ok(response);
        }
    }
}
