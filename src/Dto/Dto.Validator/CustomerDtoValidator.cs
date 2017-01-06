using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Metadata;
using System.Web.Http.Validation;

namespace Dto.Validator
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerDtoValidator : ModelValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="validatorProviders"></param>
        public CustomerDtoValidator(IEnumerable<ModelValidatorProvider> validatorProviders) : base(validatorProviders)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        public override IEnumerable<ModelValidationResult> Validate(ModelMetadata metadata, object container)
        {
            var validationResult = new List<ModelValidationResult>();

            var customer = (CustomerDto)container;

            switch (metadata.PropertyName)
            {
                case "Name":
                    NameCheck(customer, validationResult);
                    break;

                case "LastName":
                    LastNameCheck(customer, validationResult);
                    break;

                case "Addresses":
                    AddressCheck(customer, validationResult);
                    break;
            }

            return validationResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="validationResult"></param>
        private static void AddressCheck(CustomerDto customer, ICollection<ModelValidationResult> validationResult)
        {
            if (customer.Addresses == null)
                validationResult.Add(new ModelValidationResult
                {
                    Message = "Address should not be null",
                    MemberName = "Address"
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="validationResult"></param>
        private static void LastNameCheck(CustomerDto customer, ICollection<ModelValidationResult> validationResult)
        {
            if (string.IsNullOrWhiteSpace(customer.LastName))
                validationResult.Add(new ModelValidationResult
                {
                    Message = "Please specify a Lastname",
                    MemberName = "Name"
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="validationResult"></param>
        private static void NameCheck(CustomerDto customer, ICollection<ModelValidationResult> validationResult)
        {
            if (string.IsNullOrWhiteSpace(customer.Name))
                validationResult.Add(new ModelValidationResult
                {
                    Message = "Please specify a Firstname",
                    MemberName = "Name"
                });
        }
    }
}