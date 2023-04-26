using Aspti.Domain.Entidades;
using Aspti.Domain.Interface.Repository;
using Aspti.Infra.Data.Context;
using Aspti.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aspti.Infra.Data.Repository
{
	public class PerfilPermissaoRepository : Repository<PerfilPermissao>, IPerfilPermissaoRepository
	{
		public PerfilPermissaoRepository(AsptiDbContext context)
			: base(context)
		{
		}

		public override void Atualizar(PerfilPermissao obj)
		{
			var objContext = Db.Set<PerfilPermissao>().Local
				.FirstOrDefault(x => x.Id == obj.Id);
			if (objContext != null)
				Db.Entry(objContext).State = EntityState.Detached;
			base.Atualizar(obj);
		}

		public bool PossuiPermissaoUsuarioLogado(
			List<Guid> perfilUsuarioLogadoIds, string permissao, string telaId)
		{
			return ObterTodos()
				.Where(x => perfilUsuarioLogadoIds.Contains(x.RoleId) && x.ClaimValue == permissao && x.ClaimType == telaId).Any();
		}



		public IQueryable<PerfilPermissao> ObterPorPerfilId(Guid perfilId)
			=> ObterTodos().Where(x => x.RoleId == perfilId);

	}
}
