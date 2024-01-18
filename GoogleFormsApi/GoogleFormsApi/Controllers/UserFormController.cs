using Application.CQRS.Commands.UserFormActions;
using Application.CQRS.Queries.UserFormActions;
using AutoMapper;
using BLL.Helpers;
using GoogleFormsApi.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoogleFormsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserFormController : Controller
    {
        private readonly IMediator _mediator;

        private readonly IMapper _mapper;

        public UserFormController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetUserForms()
        {
            var holderId = User.GetUserIdFromPrincipal();
            var query = new GetByUser.Query() { UserId = holderId };
            var result = await _mediator.Send(query);

            var mappedResult = result.Select(_mapper.Map<UserFormResponse>);
            return Ok(mappedResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var query = new GetById.Query() { Id = id };
            var result = await _mediator.Send(query);

            var mappedResult = _mapper.Map<UserFormResponse>(result);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{formId}")]
        public async Task<IActionResult> GetByFormId([FromRoute] Guid formId)
        {
            var query = new GetByForm.Query() { FormId = formId };
            var result = await _mediator.Send(query);

            var mappedResult = result.Select(_mapper.Map<UserFormResponse>);
            return Ok(mappedResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserForm([FromRoute] Guid id)
        {
            await _mediator.Send(new Delete.Command { Id = id });

            return Ok();
        }
    }
}
