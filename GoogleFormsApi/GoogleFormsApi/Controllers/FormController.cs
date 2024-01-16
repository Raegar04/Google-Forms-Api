using Application.CQRS.Commands.FormActions;
using Application.CQRS.Queries.FormActions;
using AutoMapper;
using BLL.Helpers;
using Domain.Models;
using GoogleFormsApi.Requests.Form;
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
    public class FormController : Controller
    {
        private readonly IMediator _mediator;

        private readonly IMapper _mapper;

        public FormController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetForm([FromQuery] GetFormFilterRequest request)
        {
            var query = new Get.Query() { Predicate = GetFormFilterExpression(request.Title, request.Description, request.HolderId)};
            var result = await _mediator.Send(query);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var mappedResult = result.Data.Select(_mapper.Map<FormResponse>);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetFormById([FromRoute] Guid id)
        {
            var query = new GetById.Query() { Id = id };
            var result = await _mediator.Send(query);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var mappedResult = _mapper.Map<FormResponse>(result.Data);
            return Ok(mappedResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateForm([FromBody] AddFormRequest request)
        {
            var holderId = User.GetUserIdFromPrincipal();
            var command = _mapper.Map<Add.Command>(request);
            command.HolderId = holderId;
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateForm([FromRoute] Guid id, [FromBody] UpdateFormRequest request)
        {
            var command = _mapper.Map<Update.Command>(request);
            command.Id = id;
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteForm([FromRoute]Guid id)
        {
            var command = new Delete.Command() { Id = id };
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }

        private Expression<Func<Form, bool>> GetFormFilterExpression(string title, string description, Guid? holderId)
        {
            title = title ?? "";
            description = description ?? "";
            if (holderId == null)
            {
                return entity => entity.Title.Contains(title) && entity.Description.Contains(description);
            }

            return entity => entity.Title.Contains(title) && entity.Description.Contains(description) && entity.HolderId == holderId;
        }
    }
}
