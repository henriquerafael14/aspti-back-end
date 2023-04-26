using Aspti.Domain.Entidades;
using Aspti.Domain.Interface.Repository;
using Aspti.Domain.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aspti.Domain.Service
{
	public class UsuarioPerfilService : Service<UsuarioPerfil>, IUsuarioPerfilService
	{
		private readonly IUsuarioPerfilRepository _userRoleRepository;

		public UsuarioPerfilService(IUsuarioPerfilRepository userRoleRepository)
			: base(userRoleRepository)
		{
			_userRoleRepository = userRoleRepository;
		}

		public void Remover(Guid? userId, Guid? roleId)
		{
			try
			{
				var usuarioPerfil = _userRoleRepository
										.ObterPorUsuarioIdPerfilId(userId, roleId);

				_userRoleRepository
					.Remover(usuarioPerfil);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		public List<Guid> ObtemIdsPerfisUsuario(Guid userId)
		{
			var perfilUsuarioLogado = _userRoleRepository
											.ObterPorUsuarioId(userId);

			var perfilUsuarioLogadoIds = perfilUsuarioLogado
											.Select(x => x.RoleId)
											.ToList();

			return perfilUsuarioLogadoIds;
		}

		public UsuarioPerfil ObterPorId(Guid? userId, Guid? roleId)
		{
			return _userRoleRepository.ObterPorUsuarioIdPerfilId(userId, roleId);
		}
	}
}
