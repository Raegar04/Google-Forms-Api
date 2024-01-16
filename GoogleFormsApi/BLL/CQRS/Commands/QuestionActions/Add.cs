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
    public class Add
    {
        public class Command : IRequest<Result<bool>>
        {
            public string Description { get; set; }

            public Guid FormId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly IRepository<Question> _repository;

            private readonly IMapper _mapper;

            private readonly IRepository<Form> _formRepository;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _repository = unitOfWork.GetRepository<Question>();
                _formRepository = unitOfWork.GetRepository<Form>();
                _mapper = mapper;
            }

            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                var question = _mapper.Map<Question>(request);

                return await _repository.AddAsync(question);
            }
        }
    }
}
