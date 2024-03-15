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
    public class GetByForm
    {
        public class Query : IRequest<IEnumerable<Question>>
        {
            public Guid FormId { get; set; }
        }

        public class GetByFormHandler : IRequestHandler<Query, IEnumerable<Question>>
        {
            private readonly IQuestionService _service;

            public GetByFormHandler(IQuestionService service)
            {
                _service = service;
            }

            public Task<IEnumerable<Question>> Handle(Query request, CancellationToken cancellationToken)
            {
                return _service.GetByForm(request.FormId);
            }
        }
    }
}
