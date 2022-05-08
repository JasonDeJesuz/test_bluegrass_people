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

namespace Bluegrass.Core.Services.PersonService
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
        
        #region OPERATIONS

        public async Task<IEnumerable<PersonDTO>> GetAllAsync()
        {
            try
            {
                var allPersonsIncludingRelationships = await _context.Persons.Include(x => x.Contact).Include(x => x.Address)
                    .Include(x => x.ProfilePicture).Select(x => _mapper.Map<PersonDTO>(x)).ToListAsync();

                var allPersons = await _context.Persons.Select(x => _mapper.Map<PersonDTO>(x)).ToListAsync();

                return allPersons;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get All Persons failed with Exception: {ex.Message}");
                throw;
            }
        }

        public async Task<PersonDTO> GetAsync(int id)
        {
            try
            {
                var personById = await _context.Persons.Where(x => x.Id == id).Select(x => _mapper.Map<PersonDTO>(x)).FirstOrDefaultAsync();
                if (personById == null)
                    throw new Exception($"Person not found with ID {id}");

                return personById;
            } catch(Exception ex)
            {
                _logger.LogError($"Get Person with ID {id} failed with Exception => {ex.Message}");
                throw;
            }
        }

        public async Task<PersonUpdateDTO> UpdateAsync(int id, PersonDTO updateData)
        {
            try
            {
                var personToUpdate = await _context.Persons.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
                if (null == personToUpdate)
                    throw new Exception($"Person not found with ID {id}");

                var originalData = _mapper.Map<PersonDTO>(personToUpdate);

                // Update the values
                personToUpdate.Name = personToUpdate.Name != updateData.Name ? updateData.Name : personToUpdate.Name;
                personToUpdate.Surname = personToUpdate.Surname != updateData.Surname ? updateData.Surname : personToUpdate.Surname;
                personToUpdate.Gender = personToUpdate.Gender != updateData.Gender ? updateData.Gender : personToUpdate.Gender;

                await _context.SaveChangesAsync();

                var newData = _mapper.Map<PersonDTO>(personToUpdate);

                // TODO: Doing the Address stuffies
                // ????: How are we going to be handling that
                // IDEA: Why don't we handle only Persons here, and then we can just return arrays that will capture changes

                return new PersonUpdateDTO()
                {
                    OriginalData = originalData,
                    NewData = newData
                };
            } catch(Exception ex) {
                _logger.LogError($"Update Person with ID {id} failed with Exception => {ex.Message}");
                throw;
            }
        }

        public async Task<PersonDTO> CreateAsync(PersonDTO createData)
        {
            try
            {
                if (null == createData)
                    throw new ArgumentNullException($"Create Data NULL");

                var personToCreate = _mapper.Map<Person>(createData);

                await _context.AddAsync(personToCreate);
                await _context.SaveChangesAsync();

                var createdPerson = _mapper.Map<PersonDTO>(personToCreate);

                return createdPerson;
            } catch(Exception ex)
            {
                _logger.LogError($"Create Person failed with Exception => {ex.Message}");
                throw;
            } 
        }

        public async Task<PersonDTO> DeleteAsync(int id)
        {
            try
            {
                var personToDelete = await this.GetAsync(id);

                _context.Remove(personToDelete);
                await _context.SaveChangesAsync();

                return personToDelete;
            } catch(Exception ex)
            {
                _logger.LogError($"Delete Person with ID {id} failed with Exception => {ex.Message}");
                throw;
            }
        }
        
        #endregion
    }
}