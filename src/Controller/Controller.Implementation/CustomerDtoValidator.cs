using System.Collections.Generic;
using Dto;
using FluentValidation;

namespace Controller.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerDtoValidator : AbstractValidator<CustomerDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public CustomerDtoValidator()
        {
            RuleFor(customer => customer.Name).NotEmpty().WithMessage("Please specify a Firstname");
            RuleFor(customer => customer.LastName).NotEmpty().WithMessage("Please specify a Lastname");
            RuleFor(customer => customer.Addresses).NotNull().WithMessage("Address should not be null");
            RuleForEach(customer => customer.Addresses).SetValidator(new AddressDtoValidator());
        }

        private static bool ValidateAddresses(ICollection<AddressDto> addressDtos)
        {
            return (addressDtos!=null && addressDtos.Count > 0);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public AddressDtoValidator()
        {
            RuleForEach(x => x.Pin).NotEmpty().WithMessage("Pin should not be empty");
        }
    }
}