namespace Aspti.Application.Request
{
	public class AlterarSenhaRequest
    {
        public string SenhaAtual { get; set; }
        public string NovaSenha { get; set; }
        public string NovaSenhaConfirmacao { get; set; }
    }
}
