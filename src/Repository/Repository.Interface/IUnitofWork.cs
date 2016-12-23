using System;

namespace Repository.Interface
{
    public interface IUnitofWork : IDisposable
    {
        ICustomerRepository CustomerRepository { get; }

        IAddressRepository AddressRepository { get; }

        int Save();
    }
}
