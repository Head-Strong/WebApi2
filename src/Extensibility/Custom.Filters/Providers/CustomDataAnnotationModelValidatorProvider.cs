using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Validation.Providers;
using Dto;
using Dto.Validator;

namespace Custom.Filters.Providers
{
    public class CustomDataAnnotationModelValidatorProvider : DataAnnotationsModelValidatorProvider
    {
        private static readonly IDictionary<Type, Type> RegisteredModelTypes;

        static CustomDataAnnotationModelValidatorProvider()
        {
            RegisteredModelTypes = new Dictionary<Type, Type>
            {
                { typeof(CustomerDto), typeof(CustomerDtoMetaData) }
            };
        }

        protected override ICustomTypeDescriptor GetTypeDescriptor(Type type)
        {
            var typeDescriptor = base.GetTypeDescriptor(type);

            if (RegisteredModelTypes.ContainsKey(type))
            {
                typeDescriptor = new AssociatedMetadataTypeTypeDescriptionProvider(
                    type, RegisteredModelTypes[type]).GetTypeDescriptor(type);
            }

            return typeDescriptor;
        }
    }
}