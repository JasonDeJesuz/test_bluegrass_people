using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bluegrass.Data.Context;
using Bluegrass.Data.Data.Models;
using Bluegrass.Shared.DTO.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bluegrass.Core.Services
{
    public class PersonService : IPersonService
    {
        #region Members
        private readonly BluegrassContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonService> _logger;
        #endregion
        
        #region Constructors
        public PersonService(BluegrassContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion
        
        #region Routes

        public async Task<IEnumerable<PersonDTO>> GetAllAsync()
        {
            try
            {
                // var allPersons = await _context.Persons.Include(x => x.Contact).Include(x => x.Address)
                //     .Include(x => x.ProfilePicture).Select(x => _mapper.Map<PersonDTO>(x)).ToListAsync();

                var allPersons = await _context.Persons.Select(x => _mapper.Map<PersonDTO>(x)).ToListAsync();

                return allPersons;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get All Persons failed with Exception: {ex.Message}");
                throw ex;
            }
        }

        public Task<PersonDTO> GetAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<PersonDTO> UpdateAsync(int id, PersonDTO updateData)
        {
            throw new System.NotImplementedException();
        }

        public Task<PersonDTO> CreateAsync(PersonDTO createData)
        {
            throw new System.NotImplementedException();
        }

        public Task<PersonDTO> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }
        
        #endregion
    }
}