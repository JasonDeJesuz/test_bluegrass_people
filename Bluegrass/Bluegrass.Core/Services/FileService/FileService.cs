using AutoMapper;
using Bluegrass.Data.Context;
using Bluegrass.Data.Data.Models;
using Bluegrass.Shared.DTO.Avatar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bluegrass.Core.Services.FileService
{
    public class FileService : IFileService
    {
        #region Members
        private readonly BluegrassContext _context;
        private readonly IMapper _mapper;
        #endregion
        
        #region Constructors

        public FileService(BluegrassContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion
        
        public async Task<AvatarDTO> UploadAvatarAsync(int personId, IFormFile file)
        {
            try
            {
                // We are always deleting and then re-adding... :)
                // So this will only be a create, not a upsert

                var newAvatar = await ExtractFileData(personId, file);

                var newDbRecord = _mapper.Map<Avatar>(newAvatar);

                await _context.AddAsync<Avatar>(newDbRecord);
                await _context.SaveChangesAsync();

                return _mapper.Map<AvatarDTO>(newDbRecord);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<AvatarDTO> ExtractFileData(int personId, IFormFile file) {
            try {
                var newAvatar = new AvatarDTO(){ DateCreated = DateTime.Now };

                using var ms = new MemoryStream();
                // Copy file to Memory Stream
                await file.CopyToAsync(ms);
                // Set the Byte Array
                newAvatar.Bytes = ms.ToArray();

                // Set the rest of the properties
                newAvatar.FileExtension = file.ContentType;
                newAvatar.Size = file.Length;
                newAvatar.Description = file.FileName;
                newAvatar.PersonId = personId;

                return newAvatar;
            } catch(Exception ex) {
                throw;
            }
        }

        public async Task<AvatarDTO> RemoveAvatarAsync(int id)
        {
            try
            {
                var avatar = await _context.Avatars.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
                if (null == avatar)
                    throw new Exception($"Cannot find avatar with ID {id}");

                var currentAvatar = _mapper.Map<AvatarDTO>(avatar);
                
                // TODO: We technically need to have a default which we can always use :)

                _context.Remove(avatar);
                await _context.SaveChangesAsync();

                return currentAvatar;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<FileContentResult> RetrieveFileAsync(int id)
        {
            try{
                var avatar = await _context.Avatars.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
                if (null == avatar)
                    throw new Exception($"Avatar with ID {id} cannot be found!");

                var fileBytes = avatar.Bytes;
                var contentType = avatar.FileExtension;

                return new FileContentResult(fileBytes, contentType);
            } catch(Exception ex) {
                throw;
            }
        }
    }
}