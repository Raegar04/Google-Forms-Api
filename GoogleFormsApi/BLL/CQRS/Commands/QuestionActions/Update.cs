using Application.Abstractions;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.QuestionActions
{
    public class Update
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }

            public string Description { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IQuestionService _service;

            private readonly IMapper _mapper;

            public Handler(IQuestionService service, IMapper mapper)
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
