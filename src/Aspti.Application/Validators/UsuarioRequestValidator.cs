using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Aspti.Application.Request;
using Aspti.Application.ValidationsAttribute;

namespace Aspti.Application.Validators
{
    public class UsuarioRequestValidator : AbstractValidator<UsuarioRequest>
    {
        public UsuarioRequestValidator()
        {
            RuleFor(x => x.NomeUsuario)
                .NotEmpty()
                .WithMessage("O nome de usuário não pode ser vazio.")
                .Length(3, 60)
                .WithMessage("O nome de usuário deve possui entre 3 e 60 caracteres.");

            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O nome não pode ser vazio.")
                .Length(3, 60)
                .WithMessage("O nome deve possui entre 3 e 60 caracteres.");

            RuleFor(x => x.Sobrenome)
                .NotEmpty()
                .WithMessage("O sobrenome não pode ser vazio.")
                .Length(3, 60)
                .WithMessage("O sobrenome deve possui entre 3 e 60 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O email não é válido.")
                .EmailAddress()
                .WithMessage("O email não é válido.");

            RuleFor(x => x.CPFCNPJ)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("O CPF não pode ser vazio.")
                .NotNull()
                .WithMessage("O CPF não pode ser vazio.");

            RuleFor(x => x.Telefone)
                .NotEmpty()
                .WithMessage("O telefone não é válido.")
                .NotNull()
                .WithMessage("O telefone não é válido.");

            RuleFor(x => x.Senha)
                .Equal(x => x.ConfirmacaoSenha)
                .WithMessage("A senha e sua confirmação deve ser iguais");

            RuleFor(x => x.Endereco)
                .NotNull()
                .WithMessage("Endereço inválido")
                .SetValidator(new EnderecoRequestValidator());

            RuleFor(x => x.DataNascimento)
                .NotNull()
                .WithMessage("Data de nascimento inválida");

            When(x => x.CPFCNPJ.Length > 11, () =>
            {
                RuleFor(x => x.CPFCNPJ)
                    .Must(CNPJ => new Cnpj(CNPJ).Valido);
            });

            When(x => x.CPFCNPJ.Length <= 11, () =>
            {
                RuleFor(x => x.CPFCNPJ)
                    .Must(CPF => new Cpf(CPF).Valido);
            });
        }
    }
}
