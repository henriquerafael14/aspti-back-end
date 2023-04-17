using Aspti.Application.Request;
using FluentValidation;

namespace Aspti.Application.Validators
{
	public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.NomeUsuarioEmail)
                .NotEmpty()
                .WithMessage("É necessário fornecer um email.");

            RuleFor(x => x.Senha)
                .NotEmpty()
                .WithMessage("É necessário fornecer uma senha.");
        }
    }
}
