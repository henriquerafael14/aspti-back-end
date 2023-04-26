using Aspti.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aspti.Domain.Interface.Repository
{
	public interface IPerfilPermissaoRepository : IRepository<PerfilPermissao>
	{
		bool PossuiPermissaoUsuarioLogado(List<Guid> perfilUsuarioLogadoIds, string permissao, string telaId);
		IQueryable<PerfilPermissao> ObterPorPerfilId(Guid perfilId);
	}
}
