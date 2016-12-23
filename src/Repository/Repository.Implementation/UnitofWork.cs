using ORM.Data;
using Repository.Implementation.AutoMapperConfigMapper;
using Repository.Interface;

namespace Repository.Implementation
{
    public class UnitofWork : IUnitofWork
    {
        private readonly TestDatabaseEntities _databaseEntities;

        public UnitofWork(TestDatabaseEntities databaseEntities, IAutoMapperConfigMapper autoMapperConfigMapper)
        {
            _databaseEntities = databaseEntities;
            CustomerRepository = new CustomerRepository(_databaseEntities, autoMapperConfigMapper);
            AddressRepository = new AddressRepository(_databaseEntities, autoMapperConfigMapper);
        }

        public void Dispose()
        {
            _databaseEntities.Dispose();
        }

        public ICustomerRepository CustomerRepository { get; }

        public IAddressRepository AddressRepository { get; }

        public int Save()
        {
            return _databaseEntities.SaveChanges();
        }
    }
}
