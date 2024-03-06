using Application.Abstractions;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.FormActions
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IFormService _service;

            public Handler(IFormService service)
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
