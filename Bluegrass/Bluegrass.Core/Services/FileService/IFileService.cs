using Bluegrass.Shared.DTO.Avatar;
using Microsoft.AspNetCore.Mvc;

namespace Bluegrass.Core.Services.FileService
{
    public interface IFileService
    {
        Task<AvatarDTO> UploadAvatarAsync(int personId, IFormFile file);
        Task<AvatarDTO> RemoveAvatarAsync(int id);
        Task<FileContentResult> RetrieveFileAsync(int id);
    }
}