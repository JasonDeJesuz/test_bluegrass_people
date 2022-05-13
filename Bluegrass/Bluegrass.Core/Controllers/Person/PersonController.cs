using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bluegrass.Core.Services.ChangeService;
using Bluegrass.Core.Services.PersonService;
using Bluegrass.Shared.DTO.Generic;
using Bluegrass.Shared.DTO.Person;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bluegrass.Core.Controllers.Person
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        #region Members

        private readonly IPersonService _personService;
        private readonly IChangeService _changeService;

        #endregion
        
        #region Constructors

        public PersonController(IPersonService personService, IChangeService changeService)
        {
            _personService = personService;
            _changeService = changeService;
        }
        
        #endregion
        
        // GET: api/Person
        [HttpGet]
        public async Task<ApiResponse> Get()
        {
            try
            {
                var persons = await _personService.GetAllAsync();

                return new ApiResponse()
                {
                    Success = true,
                    Data = persons
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

        // GET: api/Person/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ApiResponse> Get(int id)
        {
            try
            {
                var persons = await _personService.GetAsync(id);

                return new ApiResponse()
                {
                    Success = true,
                    Data = persons
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

        // POST: api/Person
        [HttpPost]
        public async Task<ApiResponse> Post([FromBody] PersonDTO createData)
        {
            try
            {
                var persons = await _personService.CreateAsync(createData);

                return new ApiResponse()
                {
                    Success = true,
                    Data = persons
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

        // PUT: api/Person/5
        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] PersonDTO updateData)
        {
            try
            {
                var persons = await _personService.UpdateAsync(id, updateData);

                // TODO: Do the updates for addresses
                // TODO: Do the updates for Contact info
                // TODO: Do the updates for the Profile Pictures

                // TODO: Use the Change Service to detect the changes that have been made...
                await _changeService.GetPersonChangesAsync(persons.OriginalData, persons.NewData);

                return new ApiResponse()
                {
                    Success = true,
                    Data = persons
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

        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                var persons = await _personService.DeleteAsync(id);

                return new ApiResponse()
                {
                    Success = true,
                    Data = persons
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
    }
}
