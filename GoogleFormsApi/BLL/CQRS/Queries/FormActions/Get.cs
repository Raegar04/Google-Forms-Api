using BLL.Abstractions;
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
    public class Get
    {
        public class Query : IRequest<Result<IEnumerable<Form>>>
        {
            public Expression<Func<Form, bool>>? Predicate { get; set; }
        }

        public class GetFormHandler : IRequestHandler<Query, Result<IEnumerable<Form>>>
        {
            private readonly IRepository<Form> _formRepository;

            public GetFormHandler(IUnitOfWork unitOfWork)
            {
                _formRepository = unitOfWork.GetRepository<Form>();
            }

            public Task<Result<IEnumerable<Form>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return _formRepository.GetAllAsync(request.Predicate);
            }
        }
    }
}
