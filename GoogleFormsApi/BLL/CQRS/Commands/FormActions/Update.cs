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

namespace Application.CQRS.Commands.FormActions
{
    public class Update
    {
        public class Command : IRequest<Result<bool>>
        {
            public Guid Id { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly IRepository<Form> _formRepository;

            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _formRepository = unitOfWork.GetRepository<Form>();
                _mapper = mapper;
            }

            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                var updatingFormResult = await _formRepository.GetByIdAsync(request.Id);
                if (!updatingFormResult.Success)
                {
                    return new Result<bool>(false, updatingFormResult.Message);
                }

                var updated = updatingFormResult.Data;
                _mapper.Map(request, updated);

                return await _formRepository.UpdateAsync(request.Id, updated);
            }
        }
    }
}
