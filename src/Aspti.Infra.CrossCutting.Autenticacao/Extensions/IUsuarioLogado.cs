using System.Collections.Generic;
using System.Security.Claims;

namespace Aspti.Infra.CrossCutting.Autenticacao.Extensions
{
	public interface IUsuarioLogado
    {
        IEnumerable<Claim> ObterClaims();
        string ObterId();
        string ObterToken();
    }
}
