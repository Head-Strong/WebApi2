using System.Collections.Generic;

namespace Dto
{
    /// <summary>
    /// Customer
    /// </summary>
    public class CustomerDto
    {
        /// <summary>
        /// Customer Unique Id. Required
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Customer Name. Required
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Customer Last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Customer Addresses
        /// </summary>
        public ICollection<AddressDto> Addresses { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    var validationResult = new List<ValidationResult>();

        //    if(string.IsNullOrWhiteSpace(Name))
        //        validationResult.Add(new ValidationResult("Please specify a Firstname"));

        //    if (string.IsNullOrWhiteSpace(LastName))
        //        validationResult.Add(new ValidationResult("Please specify a Lastname"));

        //    if (Addresses == null)
        //        validationResult.Add(new ValidationResult("Address should not be null"));

        //    return validationResult;
        //}
    }
}
