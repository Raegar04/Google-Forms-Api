using BLL.Abstractions;
using BLL.Helpers;
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
        public class Query : IRequest<Result<Form>>
        {
            public Guid Id { get; set; }
        }

        public class GeFormByIdHandler : IRequestHandler<Query, Result<Form>>
        {
            private readonly IRepository<Form> _formRepository;

            public GeFormByIdHandler(IUnitOfWork unitOfWork)
            {
                _formRepository = unitOfWork.GetRepository<Form>();
            }

            public Task<Result<Form>> Handle(Query request, CancellationToken cancellationToken)
            {
                return _formRepository.GetByIdAsync(request.Id);
            }
        }
    }
}
