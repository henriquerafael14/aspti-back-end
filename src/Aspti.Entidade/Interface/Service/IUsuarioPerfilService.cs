using Aspti.Domain.Entidades;
using System;
using System.Collections.Generic;

namespace Aspti.Domain.Interface.Service
{
	public interface IUsuarioPerfilService : IService<UsuarioPerfil>
	{
		void Remover(Guid? userId, Guid? roleId);
		UsuarioPerfil ObterPorId(Guid? userId, Guid? roleId);
		List<Guid> ObtemIdsPerfisUsuario(Guid userId);
	}
}
