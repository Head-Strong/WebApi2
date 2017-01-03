using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using AutoMapper;
using ORM.Data;
using Repository.Implementation.AutoMapperConfigMapper;
using Repository.Interface;

namespace Repository.Implementation
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly TestDatabaseEntities _context;
        private readonly IMapper _autoMapperConfigMapper;

        public CustomerRepository(TestDatabaseEntities context, IAutoMapperConfigMapper autoMapperConfigMapper) : base(context)
        {
            _context = context;
            _autoMapperConfigMapper = autoMapperConfigMapper.ConfigureMap();
        }

        public string GetData()
        {
            return "Success";
        }

        public IEnumerable<Domains.Customer> GetCustomers()
        {
            var customerDb = GetEntities();

            var customers = customerDb.ToList().Select(customerDbValue => _autoMapperConfigMapper.Map<Customer, Domains.Customer>(customerDbValue)).ToList();

            return customers;
        }

        public Domains.Customer SaveCustomer(Domains.Customer customer)
        {
            var customerDb = _autoMapperConfigMapper.Map<Domains.Customer, Customer>(customer);

            _context.Customers.Add(customerDb);

            return _autoMapperConfigMapper.Map<Customer, Domains.Customer>(customerDb);
        }

        public List<string> GetProfiles()
        {
            List<string> profiles;
            var conString = ConfigurationManager.AppSettings["TestDatabaseEntities"];   //ConfigurationManager.AppSettings["MasterDB"];
            using (var con = new SqlConnection(conString))
            {
                var cmd = new SqlCommand
                {
                    CommandText = QueryData,
                    Connection = con,
                    CommandType = CommandType.Text                    
                };
                con.Open();
                var dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (!dataReader.HasRows) return null;

                profiles = new List<string>();
                while (dataReader.Read())
                {
                    profiles.Add(dataReader.GetValue(0).ToString());
                }
            }

            return profiles;
            //return _context.Database.SqlQuery<string>(QueryData).ToList();
        }

        private const string QueryData = @"SELECT DISTINCT R.roleName FROM Profiles P
                                        INNER JOIN Profile_Role_Link PRL
                                        ON P.idProfile = PRL.idProfile
                                        INNER JOIN Roles R
                                        ON R.idRole = PRL.idRole";
    }
}
