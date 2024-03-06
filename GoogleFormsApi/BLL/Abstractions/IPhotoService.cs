using Application.Helpers;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IPhotoService : IGenericService<Photo>
    {
        public Task DeleteAsync(object id);

        Task AddPhotos(List<Photo> photos);
    }
}
