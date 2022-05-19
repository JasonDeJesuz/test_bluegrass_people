using Bluegrass.Core.Services.FileService;
using Bluegrass.Core.Services.PersonService;
using Bluegrass.Data.Authentication;
using Bluegrass.Shared.DTO.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bluegrass.Core.Controllers.Files;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    #region Members
    private readonly ILogger<FilesController> _logger;
    private readonly IPersonService _personService;
    private readonly IFileService _fileService;
    #endregion

    #region Constructors
    public FilesController(ILogger<FilesController> logger, IPersonService personService, IFileService fileService)
    {
        _logger = logger;
        _personService = personService;
        _fileService = fileService;
    }
    #endregion

    // POST: api/Files
    [HttpPost("{id}", Name = "PostFiles")]
    [Authorize(Roles = ApplicationUserRoles.Admin)]
    public async Task<ApiResponse> Post(int id, IFormFile file)
    {
        try
        {
            var persons = await _personService.GetAsync(id);


            // TODO: Post the file here
            if (file.Length <= 0)
                throw new ArgumentNullException($"Cannot post a file if the data is null.");

            if (null != persons.Avatar)
                await _fileService.RemoveAvatarAsync(persons.Avatar.Id);

            var newAvatar = await _fileService.UploadAvatarAsync(id, file);

            return new ApiResponse()
            {
                Success = true,
                Data = newAvatar
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse()
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    // GET: api/files
    [HttpGet("{id}", Name = "GetFile")]
    public async Task<FileContentResult> GetFile(int id) {
        try {
            var fileToReturn = await _fileService.RetrieveFileAsync(id);

            return fileToReturn;
        } catch(Exception ex) {
            // TODO: Return a static image
            return null;
        }
    }
}