using Aspti.Infra.CrossCutting.Enums;

namespace Aspti.Domain.Interface.Service
{
	public interface IAutorizacaoPermissaoService
	{
		bool Autorizar(string controller, PermissoesEnum permissao);
	}
}
