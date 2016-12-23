using ORM.Data;
using Repository.Implementation.AutoMapperConfigMapper;
using Repository.Interface;

namespace Repository.Implementation
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly TestDatabaseEntities _context;
        private readonly IAutoMapperConfigMapper _autoMapperConfigMapper;

        public CustomerRepository(TestDatabaseEntities context, IAutoMapperConfigMapper autoMapperConfigMapper) : base(context, autoMapperConfigMapper)
        {
            _context = context;
            _autoMapperConfigMapper = autoMapperConfigMapper;
        }       
    }
}
