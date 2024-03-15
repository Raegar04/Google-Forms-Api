using Application.Abstractions;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.ResponseActions
{
    public class AddRange
    {
        public class Command : IRequest
        {
            public List<Response> Answers { get; set; }

            public Guid FormId { get; set; }

            public Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IResponseService _service;

            private readonly IUserFormService _userFormService;

            public Handler(IResponseService service, IUserFormService userFormService)
            {
                _service = service;
                _userFormService = userFormService;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            { 
                var userFormId = Guid.NewGuid();
                await _userFormService.AddAsync(new UserForm { Id = userFormId, FormId = request.FormId, UserId = request.UserId });

                for (var i = 0; i < request.Answers.Count; i++)
                {
                    request.Answers[i].UserFormId = userFormId;
                }

                await _service.AddRangeAsync(request.Answers);
            }
        }
    }
}
