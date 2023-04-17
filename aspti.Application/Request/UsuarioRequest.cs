using Aspti.Application.Validators;
using System;
using System.ComponentModel.DataAnnotations;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Aspti.Application.Request
{
	public class UsuarioRequest
    {
        private readonly UsuarioRequestValidator _validator;

        public UsuarioRequest()
        {
            _validator = new UsuarioRequestValidator();
        }

        public string NomeUsuario { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPFCNPJ { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataNascimento { get; set; }
        public EnderecoRequest Endereco { get; set; }

        public bool EhValido() => Validar().IsValid;

        public ValidationResult Validar() => _validator.Validate(this);
    }
}
