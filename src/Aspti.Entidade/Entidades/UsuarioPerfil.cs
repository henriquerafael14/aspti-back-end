using Microsoft.AspNetCore.Identity;
using System;

namespace Aspti.Domain.Entidades
{
	public class UsuarioPerfil : IdentityUserRole<Guid>
	{
		public virtual Perfil Perfil { get; protected set; }

		public void AlterarPerfil(Perfil perfil)
		{
			Perfil = perfil;
		}
	}
}