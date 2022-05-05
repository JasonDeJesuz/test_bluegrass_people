using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bluegrass.Core.Services;
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

        #endregion
        
        #region Constructors

        public PersonController(IPersonService personService)
        {
            _personService = personService;
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
            return new ApiResponse();
        }

        // POST: api/Person
        [HttpPost]
        public async Task<ApiResponse> Post([FromBody] PersonDTO createData)
        {
            return new ApiResponse();
        }

        // PUT: api/Person/5
        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] PersonDTO updateData)
        {
            return new ApiResponse();
        }

        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            return new ApiResponse();
        }
    }
}
