using System;
using Bluegrass.Core.Extensions;
using Bluegrass.Shared.DTO.Change;
using Bluegrass.Shared.DTO.Person;
using Bluegrass.Shared.DTO.Variance;

namespace Bluegrass.Core.Services.ChangeService
{
    public class ChangeService : IChangeService
    {
        #region Members

        private readonly ILogger<ChangeService> _logger;

        #endregion
        public ChangeService()
        {
        }

        public async Task<List<VarianceDTO>> GetPersonChangesAsync(PersonDTO originalData, PersonDTO newData)
        {
            try
            {
                // we need to take the old data, and then compare with the new data and add it do the list of changes
                var variances = new List<VarianceDTO>();
                // string[] changesToDetect = { "Name", "Surname", "Gender", "Country", "City", "Email", "Mobile" };
                string[] excludedList = {"DateModified", "DateCreated", "Id"};

                var personVariances = originalData.Compare(newData);
                var addressVariances = originalData.Address.Compare(newData.Address);
                var contactVariances = originalData.Contact.Compare(newData.Contact);

                variances.AddRange(personVariances);
                variances.AddRange(addressVariances);
                variances.AddRange(contactVariances);

                var cleanedData = variances.Where(x => !excludedList.Contains(x.Prop) && !x.valA.Equals(x.valB)).ToList();

                return cleanedData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
