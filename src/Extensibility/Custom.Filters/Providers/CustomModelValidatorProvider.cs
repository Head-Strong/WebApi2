using System;
using System.Collections.Generic;
using Dto;
using Dto.Validator;
using ModelMetadata = System.Web.Http.Metadata.ModelMetadata;
using ModelValidator = System.Web.Http.Validation.ModelValidator;
using ModelValidatorProvider = System.Web.Http.Validation.ModelValidatorProvider;

namespace Custom.Filters.Providers
{
    public class CustomModelValidatorProvider : ModelValidatorProvider
    {
        private static readonly IDictionary<Type, Type> RegisteredModelTypes;

        static CustomModelValidatorProvider()
        {
            RegisteredModelTypes = new Dictionary<Type, Type>
            {
                { typeof(CustomerDto), typeof(CustomerDtoValidator) },
                { typeof(AddressDto), typeof(AddressDtoValidator) }
            };
        }

        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders)
        {
            if (metadata.ContainerType != null && RegisteredModelTypes.ContainsKey(metadata.ContainerType))
            {
                var validator = RegisteredModelTypes[metadata.ContainerType];
                yield return (ModelValidator)Activator.CreateInstance(validator, validatorProviders);
            }
        }
    }
}
