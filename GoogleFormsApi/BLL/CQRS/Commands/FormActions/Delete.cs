using BLL.Abstractions;
using BLL.Helpers;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.FormActions
{
    public class Delete
    {
        public class Command : IRequest<Result<bool>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly IRepository<Form> _repository;

            public Handler(IUnitOfWork unitOfWork)
            {
                _repository = unitOfWork.GetRepository<Form>();
            }

            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _repository.DeleteAsync(request.Id);
            }
        }
    }
}
