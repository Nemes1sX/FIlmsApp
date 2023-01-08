using AutoMapper;
using FIlmsApp.Data;

namespace FIlmsApp.Services
{
    public abstract class BaseService
    {
        protected readonly FilmsContext _db;
        protected readonly IMapper _mapper;

        public BaseService(FilmsContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
    } 
}
