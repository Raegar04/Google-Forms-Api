using Application.Abstractions;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.FormActions
{
    public class Add
    {
        public class Command : IRequest
        {
            public string Title { get; set; }

            public bool Closed { get; set; }

            public string Description { get; set; }

            public Guid HolderId { get; set; }
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
                var form = _mapper.Map<Form>(request);

                await _service.AddAsync(form);
            }
        }
    }
}
