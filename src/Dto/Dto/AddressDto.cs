using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class AddressDto
    {
        public int AddressId { get; set; }

        public int CustomerId { get; set; }

        public string Pin { get; set; }        
    }
}