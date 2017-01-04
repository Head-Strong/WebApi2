using System.Collections.Generic;
using System.Web.Http.Metadata;
using System.Web.Http.Validation;

namespace Dto.Validator
{

    /// <summary>
    /// 
    /// </summary>
    public class AddressDtoValidator : ModelValidator
    {
        public AddressDtoValidator(IEnumerable<ModelValidatorProvider> validatorProviders) : base(validatorProviders)
        {
        }

        public override IEnumerable<ModelValidationResult> Validate(ModelMetadata metadata, object container)
        {
            return new List<ModelValidationResult>();
        }
    }
}