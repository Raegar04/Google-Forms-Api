using Application.Abstractions;
using AutoMapper;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.FormActions
{
    public class Update
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IFormService _service;

            private readonly IMapper _mapper;

            public Handler(IFormService service, IMapper mapper)
            {
                _service = service;
                _mapper = mapper;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var updated = await _service.GetByIdAsync(request.Id);
                _mapper.Map(request, updated);

                await _service.UpdateAsync(updated);
            }
        }
    }
}
