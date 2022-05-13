using System;
using System.Threading.Tasks;
using Bluegrass.Shared.DTO.Change;
using Bluegrass.Shared.DTO.Person;
using Bluegrass.Shared.DTO.Variance;

namespace Bluegrass.Core.Services.ChangeService
{
    public interface IChangeService
    {
        public Task<List<VarianceDTO>> GetPersonChangesAsync(PersonDTO originalData, PersonDTO newData);
    }
}
