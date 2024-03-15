using Application.Abstractions;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.QuestionActions
{
    public class GetById
    {
        public class Query : IRequest<Question?>
        {
            public Guid Id { get; set; }
        }

        public class GeFormByIdHandler : IRequestHandler<Query, Question?>
        {
            private readonly IQuestionService _service;

            public GeFormByIdHandler(IQuestionService service)
            {
                _service = service;
            }

            public Task<Question?> Handle(Query request, CancellationToken cancellationToken)
            {
                return _service.GetByIdAsync(request.Id);
            }
        }
    }
}
