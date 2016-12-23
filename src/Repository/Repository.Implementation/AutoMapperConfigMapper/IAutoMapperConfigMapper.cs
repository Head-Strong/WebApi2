using AutoMapper;

namespace Repository.Implementation.AutoMapperConfigMapper
{
    public interface IAutoMapperConfigMapper
    {
        IMapper ConfigureMap();
    }

    public class AutoMapperConfigMapper : IAutoMapperConfigMapper
    {
        public IMapper ConfigureMap()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ORM.Data.Customer, Domains.Customer>();
                //.ForMember(x => x.Addresses, opt => opt.Ignore());
                cfg.CreateMap<ORM.Data.Address, Domains.Address>().
                    ForSourceMember(x => x.Customer, opt => opt.Ignore());

                cfg.CreateMap<Domains.Customer, ORM.Data.Customer>();
                //.ForMember(x => x.Addresses, opt => opt.Ignore());
                cfg.CreateMap<Domains.Address, ORM.Data.Address>().
                    ForMember(x => x.Customer, opt => opt.Ignore());
            });

            var mapper = config.CreateMapper();

            return mapper;
        }
    }
}
