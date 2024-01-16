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

namespace Application.CQRS.Queries.UserFormActions
{
    public class Get
    {
        public class Query : IRequest<Result<IEnumerable<UserForm>>>
        {
            public Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<IEnumerable<UserForm>>>
        {
            private readonly IRepository<UserForm> _repository;

            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _repository = unitOfWork.GetRepository<UserForm>();
                _mapper = mapper;
            }

            public async Task<Result<IEnumerable<UserForm>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userFormResult = await _repository.GetAllAsync(item=>item.UserId == request.UserId);
                if (!userFormResult.Success)
                {
                    return new Result<IEnumerable<UserForm>>(false, "Cannot get user's forms");
                }

                return new Result<IEnumerable<UserForm>>(true, userFormResult.Data);
            }
        }
    }
}
