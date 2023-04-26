using System;

namespace Aspti.Application.Response
{
	public class LoginResponse
    {
        public Guid Id { get; set; }
        public string AccessToken { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }
    }
}
