using System.ComponentModel.DataAnnotations;

namespace BlazorForms.Utils.CustomValidations
{
    public class CeroValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            if (value is decimal @decimal)
            {
                return @decimal > 0;
            }

            if (value is int)
            {
                return (int)value > 0;
            }

            if (value is double)
            {
                return (double)value > 0;
            }

            if (value is float)
            {
                return (float)value > 0;
            }

            if (value is long)
            {
                return (long)value > 0;
            }

            if (value is short)
            {
                return (short)value > 0;
            }

            if (value is decimal?)
            {
                return (decimal?)value > 0;
            }

            if (value is int?)
            {
                return (int?)value > 0;
            }

            if (value is double?)
            {
                return (double?)value > 0;
            }

            if (value is float?)
            {
                return (float?)value > 0;
            }

            if (value is long?)
            {
                return (long?)value > 0;
            }

            if (value is short?)
            {
                return (short?)value > 0;
            }

            if (value is byte?)
            {
                return (byte?)value > 0;
            }

            return false;
        }
    }
}
