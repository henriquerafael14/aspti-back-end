using Aspti.Domain.Entidades;
using Aspti.Domain.Interface.Repository;
using Aspti.Infra.Data.Context;
using Aspti.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Aspti.Infra.Data.Repository
{
	public class UsuarioPerfilRepository : Repository<UsuarioPerfil>, IUsuarioPerfilRepository
	{
		public UsuarioPerfilRepository(AsptiDbContext context)
			: base(context)
		{
		}

		public override void Atualizar(UsuarioPerfil obj)
		{
			var objContext = Db.Set<UsuarioPerfil>().Local
				.FirstOrDefault(x => x.UserId == obj.UserId && x.RoleId == obj.RoleId);
			if (objContext != null)
				Db.Entry(objContext).State = EntityState.Detached;
			base.Atualizar(obj);
		}

		public IQueryable<UsuarioPerfil> ObterPorUsuarioId(Guid userId)
			=> ObterTodos().Where(x => x.UserId == userId);

		public UsuarioPerfil ObterPorUsuarioIdPerfilId(Guid? userId, Guid? roleId)
			=> ObterTodos().FirstOrDefault(x => x.UserId == userId && x.RoleId == roleId);
	}

}
