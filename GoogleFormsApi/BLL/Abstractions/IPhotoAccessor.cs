using Application.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IPhotoAccessor
    {
        Task<PhotoUploadResult> UploadPhoto(FormFile file);

        Task DeletePhoto(string id);
    }
}
