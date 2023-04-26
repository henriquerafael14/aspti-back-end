using Aspti.Domain.Entidades;
using Aspti.Infra.CrossCutting.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aspti.Domain.Interface.Service
{
	public interface IPerfilPermissaoService : IService<PerfilPermissao>
	{
		bool PossuiPermissaoUsuarioLogado(List<Guid> perfilUsuarioLogadoIds, PermissoesEnum permissao, string telaId);
		Task AtualizarPermissoes(Guid perfilId, List<PerfilPermissao> permissoes);
	}
}
