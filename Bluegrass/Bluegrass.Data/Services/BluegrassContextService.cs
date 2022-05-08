using Bluegrass.Data.Context;

namespace Bluegrass.Data.Services
{
    public class BluegrassContextService : IBluegrassContextService
    {
        #region Members
        private readonly BluegrassContext _context;
        #endregion

        #region Constructors
        public BluegrassContextService(BluegrassContext context)
        {
            _context = context;
        }
        #endregion
         
        public BluegrassContext GetDatabaseContext()
        {
            return _context;
        }
    }
}