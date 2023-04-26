using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aspti.Application.Response
{
	public class UsuarioResponse
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string CPFCNPJ { get; set; }

        public string RG { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataNascimento { get; set; }

        public EnderecoResponse Endereco { get; set; }

        public List<PerfilResponse> Perfis { get; set; }
    }
}
