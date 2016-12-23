using System.Collections.Generic;

namespace Dto
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public ICollection<AddressDto> Addresses { get; set; }        
    }
}
