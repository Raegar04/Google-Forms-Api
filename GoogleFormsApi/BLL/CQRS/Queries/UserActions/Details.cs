using Application.Responses;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.UserActions
{
    public class Details
    {
        public class Query : IRequest<UserDetailsResponse> 
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, UserDetailsResponse>
        {
            private readonly UserManager<AppUser> _userManager;

            private readonly IMapper _mapper;

            public Handler(UserManager<AppUser> userManager, IMapper mapper)
            {
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<UserDetailsResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.Id.ToString());
                ArgumentNullException.ThrowIfNull(user);

                var userDetailsResponse = _mapper.Map<UserDetailsResponse>(user);
                return userDetailsResponse;
            }
        }
    }
}
