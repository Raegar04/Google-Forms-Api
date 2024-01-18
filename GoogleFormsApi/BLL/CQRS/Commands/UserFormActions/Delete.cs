using Application.Abstractions;
using BLL.Helpers;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.UserFormActions
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IUserFormService _service;

            public Handler(IUserFormService service)
            {
                _service = service;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                await _service.DeleteAsync(request.Id);
            }
        }
    }
}
