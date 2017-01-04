using System;
using System.Collections.Generic;
using System.Web.Http.Metadata;
using System.Web.Http.Validation;
using Dto;
using Dto.Validator;

namespace Custom.Filters
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
