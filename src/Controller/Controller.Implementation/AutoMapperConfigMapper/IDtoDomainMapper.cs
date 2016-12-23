using AutoMapper;
using Dto;

namespace Controller.Implementation.AutoMapperConfigMapper
{
    public interface IDtoDomainMapper
    {
        IMapper ConfigureMap();
    }

    public class DtoDomainMapper : IDtoDomainMapper
    {
        public IMapper ConfigureMap()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Dto.CustomerDto, Domains.Customer>();
                //.ForMember(x => x.Addresses, opt => opt.Ignore());
                cfg.CreateMap<AddressDto, Domains.Address>();

                cfg.CreateMap<Domains.Customer, CustomerDto>();
                cfg.CreateMap<Domains.Address, Dto.AddressDto>();
            });

            var mapper = config.CreateMapper();

            return mapper;
        }
    }
}
