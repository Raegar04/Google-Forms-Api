using Application.Abstractions;
using Application.Helpers;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.PhotoActions
{
    public class Upload
    {
        public class Command : IRequest
        {
            public IFormFile File { get; set; }

            public bool IsMain { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IPhotoAccessor _photoAccessor;
            private readonly IPhotoService _photoService;
            private readonly IHttpContextAccessor _contextAccessor;

            public Handler(IPhotoAccessor photoAccessor, IPhotoService photoService, IHttpContextAccessor contextAccessor)
            {
                _photoAccessor = photoAccessor;
                _photoService = photoService;
                _contextAccessor = contextAccessor;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var principal = _contextAccessor?.HttpContext?.User;
                ArgumentNullException.ThrowIfNull(principal, "User");

                var uploadPhotoResult = await _photoAccessor.UploadPhoto((FormFile)request.File);
                var authUserId = principal.GetUserIdFromPrincipal();
                var photo = new Photo()
                {
                    Id = uploadPhotoResult.PublicId,
                    Url = uploadPhotoResult.SecureUrl,
                    AppUserId = authUserId,
                    IsMain = request.IsMain
                };

                await _photoService.AddAsync(photo);
            }
        }
    }
}
