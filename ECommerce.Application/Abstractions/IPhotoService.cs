using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Abstractions
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> UploadPhotoAsync(IFormFile photo);
      
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
