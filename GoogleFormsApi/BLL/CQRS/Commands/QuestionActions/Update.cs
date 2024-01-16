using AutoMapper;
using BLL.Abstractions;
using BLL.Helpers;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.QuestionActions
{
    public class Update
    {
        public class Command : IRequest<Result<bool>>
        {
            public Guid Id { get; set; }

            public string Description { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly IRepository<Question> _repository;

            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _repository = unitOfWork.GetRepository<Question>();
                _mapper = mapper;
            }

            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                var updatingResult = await _repository.GetByIdAsync(request.Id);
                if (!updatingResult.Success)
                {
                    return new Result<bool>(false, updatingResult.Message);
                }

                var updated = updatingResult.Data;
                _mapper.Map(request, updated);

                return await _repository.UpdateAsync(request.Id, updated);
            }
        }
    }
}
