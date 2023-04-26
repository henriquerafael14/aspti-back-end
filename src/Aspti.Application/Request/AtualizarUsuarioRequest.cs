using Aspti.Application.Validators;
using System;
using System.ComponentModel.DataAnnotations;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Aspti.Application.Request
{
	public class AtualizarUsuarioRequest
    {
        private readonly AtualizarUsuarioRequestValidator _validator;

        public AtualizarUsuarioRequest()
        {
            _validator = new AtualizarUsuarioRequestValidator();
        }

        public Guid Id { get; set; }

        public string NomeUsuario { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string CPFCNPJ { get; set; }

        public string RG { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataNascimento { get; set; }

        public EnderecoRequest Endereco { get; set; }

        public bool EhValido() => Validar().IsValid;

        public ValidationResult Validar() => _validator.Validate(this);
    }
}
