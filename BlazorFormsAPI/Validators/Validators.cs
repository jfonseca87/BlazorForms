using BlazorFormsAPI.Models;
using FluentValidation;

namespace BlazorFormsAPI.Validators
{
    public class PersonValidator: AbstractValidator<Person>
    {
        const string FIELD_IS_REQUIRED = "{PropertyName} is required";

        public PersonValidator()
        {
            RuleFor(p => p.FirstName)
                .Must(v => !string.IsNullOrEmpty(v))
                .WithMessage(FIELD_IS_REQUIRED);

            RuleFor(p => p.LastName)
                .Must(v => !string.IsNullOrEmpty(v))
                .WithMessage(FIELD_IS_REQUIRED);

            RuleFor(p => p.Email)
                .Must(v => !string.IsNullOrEmpty(v))
                .WithMessage(FIELD_IS_REQUIRED);
            
            RuleFor(p => p.Email)
                .EmailAddress()
                .WithMessage("Email is not a valid email address");
        }
    }
}
