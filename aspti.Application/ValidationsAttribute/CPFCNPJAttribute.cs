using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspti.Application.ValidationsAttribute
{
    public class CPFCNPJAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            if (string.IsNullOrEmpty(value?.ToString()))
                return new ValidationResult(ErrorMessage);

            bool valido = value?.ToString().Length > 11 ?
             new Cnpj(value?.ToString()).Valido :
             new Cpf(value?.ToString()).Valido;

            return valido ?
                 new ValidationResult(ErrorMessage) :
                 ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            var error = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-error", error);
        }
    }
}
