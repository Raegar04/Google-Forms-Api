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
    public class GetById
    {
        public class Query : IRequest<UserForm?>
        {
            public Guid Id { get; set; }
        }

        public class GeFormByIdHandler : IRequestHandler<Query, UserForm?>
        {
            private readonly IUserFormService _service;

            public GeFormByIdHandler(IUserFormService service)
            {
                _service = service;
            }

            public Task<UserForm?> Handle(Query request, CancellationToken cancellationToken)
            {
                return _service.GetByIdAsync(request.Id);
            }
        }
    }
}
