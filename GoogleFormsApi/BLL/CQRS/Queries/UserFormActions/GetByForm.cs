using Application.Abstractions;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.UserFormActions
{
    public class GetByForm
    {
        public class Query : IRequest<IEnumerable<UserForm>>
        {
            public Guid FormId { get; set; }
        }

        public class GetByFormHandler : IRequestHandler<Query, IEnumerable<UserForm>>
        {
            private readonly IUserFormService _service;

            public GetByFormHandler(IUserFormService service)
            {
                _service = service;
            }

            public Task<IEnumerable<UserForm>> Handle(Query request, CancellationToken cancellationToken)
            {
                return _service.GetByForm(request.FormId);
            }
        }
    }
}
