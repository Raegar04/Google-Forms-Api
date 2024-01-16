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
            var query = new Get.Query() { UserId = holderId };
            var result = await _mediator.Send(query);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var mappedResult = result.Data.Select(_mapper.Map<UserFormResponse>);
            return Ok(mappedResult);
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
