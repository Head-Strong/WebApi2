using System.ComponentModel.DataAnnotations;

namespace Dto.Validator
{
    public class CustomerDtoMetaData
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name Required!!!")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "LastName Required!!!")]
        public string LastName { get; set; }
    }
}
