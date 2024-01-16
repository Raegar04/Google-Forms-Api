using BLL.Abstractions;
using BLL.Helpers;
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
        public class Query : IRequest<Result<IEnumerable<Question>>>
        {
            public Guid FormId { get; set; }
        }

        public class GetByFormHandler : IRequestHandler<Query, Result<IEnumerable<Question>>>
        {
            private readonly IRepository<Question> _questionRepository;

            public GetByFormHandler(IUnitOfWork unitOfWork)
            {
                _questionRepository = unitOfWork.GetRepository<Question>();
            }

            public Task<Result<IEnumerable<Question>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return _questionRepository.GetAllAsync(q => q.FormId == request.FormId);
            }
        }
    }
}
