using Application.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.PhotoActions
{
    public class Delete
    {
        public class Command : IRequest 
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IPhotoAccessor _photoAccessor;
            private readonly IPhotoService _photoService;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IPhotoAccessor photoAccessor ,IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
            {
                _photoAccessor = photoAccessor;
                _photoService = photoService;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var principal = _httpContextAccessor?.HttpContext?.User;
                ArgumentNullException.ThrowIfNull(principal, "User");

                await _photoAccessor.DeletePhoto(request.Id);
                await _photoService.DeleteAsync(request.Id);
            }
        }
    }
}
