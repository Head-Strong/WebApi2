using ORM.Data;
using Repository.Implementation.AutoMapperConfigMapper;
using Repository.Interface;

namespace Repository.Implementation
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        private readonly TestDatabaseEntities _dbContext;
        private readonly IAutoMapperConfigMapper _autoMapperConfigMapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="autoMapperConfigMapper"></param>
        public AddressRepository(TestDatabaseEntities dbContext, IAutoMapperConfigMapper autoMapperConfigMapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _autoMapperConfigMapper = autoMapperConfigMapper;
        }
    }
}