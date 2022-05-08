using System;
using System.Threading.Tasks;
using Bluegrass.Shared.DTO.Change;

namespace Bluegrass.Core.Services.ChangeService
{
    public interface IChangeService
    {
        public Task<ChangeDTO> GetChangesAsync(Object originalData, Object newData);
    }
}
