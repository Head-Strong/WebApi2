using System;
using ORM.Data;
using Repository.Implementation.AutoMapperConfigMapper;
using Repository.Interface;

namespace Repository.Implementation
{
    public class UnitofWork : IUnitofWork
    {
        private readonly TestDatabaseEntities _databaseEntities;
        private bool _disposed = false;

        public UnitofWork(TestDatabaseEntities databaseEntities, IAutoMapperConfigMapper autoMapperConfigMapper)
        {
            _databaseEntities = databaseEntities;
            CustomerRepository = new CustomerRepository(_databaseEntities, autoMapperConfigMapper);
            AddressRepository = new AddressRepository(_databaseEntities, autoMapperConfigMapper);
        }

        public ICustomerRepository CustomerRepository { get; }

        public IAddressRepository AddressRepository { get; }

        public int Save()
        {
            return _databaseEntities.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _databaseEntities.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
