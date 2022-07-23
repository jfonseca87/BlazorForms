using System.ComponentModel.DataAnnotations;

namespace BlazorForms.Utils.CustomValidations
{
    public class DependentValidationAttribute : ValidationAttribute
    {
        private readonly string _dependentProperty;
        private readonly string _dependentValue;

        public DependentValidationAttribute(string dependentProperty, string dependentValue)
        {
            _dependentProperty = dependentProperty;
            _dependentValue = dependentValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dependentProperty = validationContext.ObjectType.GetProperty(_dependentProperty);
            var dependentValue = dependentProperty.GetValue(validationContext.ObjectInstance, null);

            if (dependentValue.ToString() != _dependentValue)
            {
                return ValidationResult.Success;
            }
            
            if (value == null)
            {
                return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
            }

            if (value is int @int && @int == 0)
            {
                return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }
}
