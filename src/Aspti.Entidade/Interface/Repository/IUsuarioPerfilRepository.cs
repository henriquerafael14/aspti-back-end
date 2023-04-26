using Aspti.Domain.Entidades;
using System;
using System.Linq;

namespace Aspti.Domain.Interface.Repository
{
	public interface IUsuarioPerfilRepository : IRepository<UsuarioPerfil>
	{
		IQueryable<UsuarioPerfil> ObterPorUsuarioId(Guid userId);
		UsuarioPerfil ObterPorUsuarioIdPerfilId(Guid? userId, Guid? roleId);
	}
}
