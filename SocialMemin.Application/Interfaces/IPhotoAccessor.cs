﻿using Microsoft.AspNetCore.Http;
using SocialMemin.Application.Photos;

namespace SocialMemin.Application.Interfaces
{
    public interface IPhotoAccessor
    {
        Task<PhotoUploadResult> AddPhoto(IFormFile file);
        Task<string> DeletePhoto(string publicId);
    }
}
