using FluentValidation.Results;
using Aspti.Application.Validators;

namespace Aspti.Application.Request
{
    public class LoginRequest
    {
        private readonly LoginRequestValidator _validator;

        public LoginRequest()
        {
            _validator = new LoginRequestValidator();
        }

        public string NomeUsuarioEmail { get; set; }
        public string Senha { get; set; }

        public bool EhValido() => Validar().IsValid;
        public ValidationResult Validar() => _validator.Validate(this);
    }
}
