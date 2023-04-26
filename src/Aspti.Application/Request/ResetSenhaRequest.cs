using System;

namespace Aspti.Application.Request
{
	public class ResetSenhaRequest
    {
        public Guid UsuarioId { get; set; }
        public string Token { get; set; }
        public string NovaSenha { get; set; }
    }
}
