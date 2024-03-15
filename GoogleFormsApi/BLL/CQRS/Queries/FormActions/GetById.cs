using Application.Abstractions;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.FormActions
{
    public class GetById
    {
        public class Query : IRequest<Form?>
        {
            public Guid Id { get; set; }
        }

        public class GeFormByIdHandler : IRequestHandler<Query, Form?>
        {
            private readonly IFormService _service;

            public GeFormByIdHandler(IFormService service)
            {
                _service = service;
            }

            public Task<Form?> Handle(Query request, CancellationToken cancellationToken)
            {
                return _service.GetByIdAsync(request.Id);

            }
        }
    }
}
