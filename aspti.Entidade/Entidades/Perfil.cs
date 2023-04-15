using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspti.Domain.Entidades
{
	public class Perfil : IdentityRole<Guid>
	{
		public bool Administrador { get; set; }
		public virtual ICollection<UsuarioPerfil> PerfilUsuarios { get; protected set; }
		public virtual ICollection<PerfilPermissao> Permissoes { get; protected set; }

		public Perfil() 
		{ }

		public Perfil(string name, bool administrador)
		{
			Name = name;
			NormalizedName = name.ToUpper();
			Administrador = administrador;
		}

        public Perfil(Guid id, string nome, ICollection<PerfilPermissao> permissoes, bool administrador)
        {
            Id = id;
            Name = nome;
            NormalizedName = nome.ToUpper();
            Permissoes = permissoes;
            Administrador = administrador;
        }

        public void AlterarPerfilUsuarios(ICollection<UsuarioPerfil> perfilUsuarios)
        {
            PerfilUsuarios = perfilUsuarios;
        }

        public void AlterarPermissoes(ICollection<PerfilPermissao> permissoes)
        {
            Permissoes = permissoes;
        }

        public void AlterarEhAdministrador(bool ehAdministrador)
        {
            Administrador = ehAdministrador;
        }
    }
}
