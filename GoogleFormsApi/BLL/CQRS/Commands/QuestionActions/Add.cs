using Application.Abstractions;
using AutoMapper;
using BLL.Helpers;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.QuestionActions
{
    public class Add
    {
        public class Command : IRequest
        {
            public string Description { get; set; }

            public Guid FormId { get; set; }
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
                var question = _mapper.Map<Question>(request);

                await _service.AddAsync(question);
            }
        }
    }
}
