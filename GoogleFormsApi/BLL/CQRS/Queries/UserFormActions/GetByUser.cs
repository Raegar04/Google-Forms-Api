using Application.Abstractions;
using AutoMapper;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.UserFormActions
{
    public class GetByUser
    {
        public class Query : IRequest<IEnumerable<UserForm>>
        {
            public Guid UserId { get; set; }
        }

        public class GetByUserHandler : IRequestHandler<Query, IEnumerable<UserForm>>
        {
            private readonly IUserFormService _service;

            public GetByUserHandler(IUserFormService service)
            {
                _service = service;
            }

            public Task<IEnumerable<UserForm>> Handle(Query request, CancellationToken cancellationToken)
            {
                return _service.GetByUser(request.UserId);
            }
        }
    }
}
