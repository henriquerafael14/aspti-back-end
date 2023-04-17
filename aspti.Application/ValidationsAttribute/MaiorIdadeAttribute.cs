using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Aspti.Application.ValidationsAttribute
{
	public class MaiorIdadeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            if (string.IsNullOrEmpty(value?.ToString()))
            {
                return new ValidationResult(ErrorMessage);
            }

            DateTime data = Convert.ToDateTime(value);
            if ((DateTime.Now.Year - data.Year) < 18)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            var error = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-error", error);
        }


    }
}
