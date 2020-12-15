using System;
using System.ComponentModel.DataAnnotations;

namespace Nasa.Data.Attributes
{
    public class DateTimeRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dateValue = (DateTime)value;

            if (dateValue.Date < new DateTime(1995, 6, 16) || dateValue.Date > DateTime.Now.Date)
            {
                return new ValidationResult("Date must be between June 16th 1995 and today.");
            }

            return ValidationResult.Success;
        }
    }
}
