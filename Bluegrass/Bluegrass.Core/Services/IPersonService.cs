using System.Collections.Generic;
using System.Threading.Tasks;
using Bluegrass.Shared.DTO.Person;

namespace Bluegrass.Core.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonDTO>> GetAllAsync();
        Task<PersonDTO> GetAsync();
        Task<PersonDTO> UpdateAsync(int id, PersonDTO updateData);
        Task<PersonDTO> CreateAsync(PersonDTO createData);
        Task<PersonDTO> DeleteAsync(int id);
    }
}