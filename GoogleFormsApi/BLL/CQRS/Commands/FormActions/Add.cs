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
    public class Add
    {
        public class Command : IRequest<Result<bool>>
        {
            public string Title { get; set; }

            public string Description { get; set; }

            public Guid HolderId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly IRepository<Form> _repository;

            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _repository = unitOfWork.GetRepository<Form>();
                _mapper = mapper;
            }

            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                var form = _mapper.Map<Form>(request);

                return await _repository.AddAsync(form);
            }
        }
    }
}
