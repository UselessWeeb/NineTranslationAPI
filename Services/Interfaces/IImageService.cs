using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Services.Interfaces
{
    public interface IImageService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile file);
    }
}
