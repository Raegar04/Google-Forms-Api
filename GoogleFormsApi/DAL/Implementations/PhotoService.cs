using Application.Abstractions;
using Application.Helpers;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Persistence.CloudinaryManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Implementations
{
    public class PhotoService : GenericService<Photo>, IPhotoService
    {
        public PhotoService(GoogleFormsDbContext context, IHttpContextAccessor accessor) : base(context, accessor)
        {
        }

        public async Task AddPhotos(List<Photo> photos)
        {
            await _context.AddRangeAsync(photos);
            await _context.SaveChangesAsync();
        }
    }
}
