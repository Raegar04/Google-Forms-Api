using Application.Abstractions;
using BLL.Helpers;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.FormActions
{
    public class GetByHolder
    {
        public class Query : IRequest<IEnumerable<Form>>
        {
            public Guid HolderId { get; set; }
        }

        public class GetFormHandler : IRequestHandler<Query, IEnumerable<Form>>
        {
            private readonly IFormService _service;

            public GetFormHandler(IFormService service)
            {
                _service = service;
            }

            public Task<IEnumerable<Form>> Handle(Query request, CancellationToken cancellationToken)
            {
                return _service.GetByHolder(request.HolderId);
            }
        }
    }
}
