using Application.CQRS.Commands.ResponseActions;
using AutoMapper;
using BLL.Helpers;
using Domain.Models;
using GoogleFormsApi.Requests.Answer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoogleFormsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ResponseController : Controller
    {
        private readonly IMediator _mediator;

        private readonly IMapper _mapper;

        public ResponseController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("{formId}")]
        public async Task<IActionResult> CreateResponseList([FromRoute] Guid formId, [FromBody] IEnumerable<AddAnswerRequest> request)
        {
            var answers = request.Select(_mapper.Map<Response>);
            var authUserId = User.GetUserIdFromPrincipal();
            var addResult = await _mediator.Send(new AddRange.Command { Answers = answers.ToList(), FormId = formId, UserId = authUserId });
            if (!addResult.Success)
            {
                return BadRequest(addResult.Message);
            }

            return Ok();
        }
    }
}
