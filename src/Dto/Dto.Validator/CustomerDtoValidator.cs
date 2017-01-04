using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using System.Web.Http.Metadata;
using System.Web.Http.Validation;

namespace Dto.Validator
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerDtoValidator : ModelValidator
    {
        public CustomerDtoValidator(IEnumerable<ModelValidatorProvider> validatorProviders) : base(validatorProviders)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        //public CustomerDtoValidator()
        //{
        //    //RuleFor(customer => customer.Name).NotEmpty().WithMessage("Please specify a Firstname");
        //    //RuleFor(customer => customer.LastName).NotEmpty().WithMessage("Please specify a Lastname");
        //    //RuleFor(customer => customer.Addresses).NotNull().WithMessage("Address should not be null");
        //    //RuleForEach(customer => customer.Addresses).SetValidator(new AddressDtoValidator());
        //}

        public override IEnumerable<ModelValidationResult> Validate(ModelMetadata metadata, object container)
        {
            var validationResult = new List<ModelValidationResult>();

            var customer = (CustomerDto) container;

            if (string.IsNullOrWhiteSpace(customer.Name))
                validationResult.Add(new ModelValidationResult
                {
                    Message = "Please specify a Firstname",
                    MemberName = "Name"
                });

            if (string.IsNullOrWhiteSpace(customer.LastName))
                validationResult.Add(new ModelValidationResult
                {
                    Message = "Please specify a Lastname",
                    MemberName = "Name"
                });

            if (customer.Addresses == null)
                validationResult.Add(new ModelValidationResult
                {
                    Message = "Address should not be null",
                    MemberName = "Address"
                });

            return validationResult;
        }
    }
}