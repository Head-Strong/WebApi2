using System.Collections.Generic;

namespace Domains
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public ICollection<Address> Addresses { get; set; }       
    }
}
