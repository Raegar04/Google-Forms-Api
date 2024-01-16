using Application.CQRS.Commands.QuestionActions;
using Application.CQRS.Queries.QuestionActions;
using AutoMapper;
using Domain.Models;
using GoogleFormsApi.Requests.Question;
using GoogleFormsApi.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace GoogleFormsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class QuestionController : Controller
    {
        private readonly IMediator _mediator;

        private readonly IMapper _mapper;

        public QuestionController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{formId}")]
        public async Task<IActionResult> GetQuestionsByFormId([FromRoute] Guid formId)
        {
            var query = new GetByForm.Query() { FormId = formId };
            var result = await _mediator.Send(query);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var mappedResult = result.Data.Select(_mapper.Map<QuestionResponse>);
            return Ok(mappedResult);
        }

        [HttpPost("{formId}")]
        public async Task<IActionResult> CreateQuestion([FromRoute]Guid formId, [FromBody] AddQuestionRequest request)
        {
            var command = _mapper.Map<Add.Command>(request);
            command.FormId = formId;
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion([FromRoute] Guid id, [FromBody] UpdateQuestionRequest updateQuestionRequest)
        {
            var command = _mapper.Map<Update.Command>(updateQuestionRequest);
            command.Id = id;
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion([FromRoute] Guid id)
        {
            var command = new Delete.Command() { Id = id };
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }
    }
}
