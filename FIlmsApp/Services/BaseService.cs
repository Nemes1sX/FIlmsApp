using FIlmsApp.Data;

namespace FIlmsApp.Services
{
    public abstract class BaseService
    {
        protected readonly FilmsContext _db;

        public BaseService(FilmsContext db)
        {
            _db = db;
        }
    } 
}
