using Bluegrass.Data.Context;

namespace Bluegrass.Data.Services
{
    public interface IBluegrassContextService
    {
        /// <summary>
        /// Directly get the will app context - Just playing around
        /// </summary>
        /// <returns></returns>
        public BluegrassContext GetDatabaseContext();
    }
}