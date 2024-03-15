using Application.Abstractions;
using Application.Helpers;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.CloudinaryManager
{
    public class PhotoAccessor : IPhotoAccessor
    {
        private readonly Cloudinary _cloudinary;

        public PhotoAccessor(IOptions<CloudinarySettings> config)
        {
            var value = config.Value;
            var account = new Account(cloud: value.CloudName, apiKey: value.API_Key, apiSecret: value.API_Secret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task DeletePhoto(string id)
        {
            var deleteParams = new DeletionParams(id);
            var result = await _cloudinary.DestroyAsync(deleteParams);

            if (result.Error != null)
            {
                throw new Exception(result.Error.Message);
            }
        }

        public async Task<PhotoUploadResult> UploadPhoto(FormFile file)
        {
            if (file.Length <= 0)
            {
                throw new ArgumentException(file.Name);
            }

            ImageUploadParams uploadParams;
            await using (var stream = file.OpenReadStream())
            {
                uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.Name, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill")
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                if (uploadResult.Error != null)
                {
                    throw new Exception(uploadResult.Error.Message);
                }

                return new PhotoUploadResult()
                {
                    PublicId = uploadResult.PublicId,
                    SecureUrl = uploadResult.SecureUrl.ToString()
                };
            }
        }
    }
}
