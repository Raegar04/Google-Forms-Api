using BLL.Abstractions;
using BLL.Helpers;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.ResponseActions
{
    public class AddRange
    {
        public class Command : IRequest<Result<bool>> 
        {
            public List<Response> Answers { get; set; }

            public Guid FormId { get; set; }

            public Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly IRepository<Response> _repository;

            private readonly IRepository<UserForm> _userFormRepository;

            public Handler(IUnitOfWork unitOfWork)
            {
                _repository = unitOfWork.GetRepository<Response>();
                _userFormRepository = unitOfWork.GetRepository<UserForm>();
            }

            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            { 
                var userFormId = Guid.NewGuid();
                var addUserFormResult = await _userFormRepository.AddAsync(new UserForm { Id = userFormId, FormId = request.FormId, UserId = request.UserId });
                if (!addUserFormResult.Success)
                {
                    return new Result<bool>(false, "Cannot establish connection between user and form");
                }

                for (var i = 0; i < request.Answers.Count; i++)
                {
                    request.Answers[i].UserFormId = userFormId;
                }

                return await _repository.AddRangeAsync(request.Answers);
            }
        }
    }
}
