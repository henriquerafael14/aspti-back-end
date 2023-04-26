using Aspti.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aspti.Infra.Data.Mapping
{
	public class UsuarioPerfilMapping : IEntityTypeConfiguration<UsuarioPerfil>
	{
		public UsuarioPerfilMapping()
		{
		}

		public void Configure(EntityTypeBuilder<UsuarioPerfil> builder)
		{
			builder.ToTable(Constantes.Constantes.Tabelas.UsuarioPerfil, Constantes.Constantes.Schemas.Identidade);
		}
	}

}
