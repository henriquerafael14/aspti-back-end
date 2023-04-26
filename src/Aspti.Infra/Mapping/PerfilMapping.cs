using Aspti.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aspti.Infra.Data.Mapping
{
	public class PerfilMapping : IEntityTypeConfiguration<Perfil>
	{
		public PerfilMapping()
		{
		}

		public void Configure(EntityTypeBuilder<Perfil> builder)
		{
			builder.ToTable(Constantes.Constantes.Tabelas.Perfil, Constantes.Constantes.Schemas.Identidade);

			builder.HasMany(u => u.PerfilUsuarios)
				.WithOne(up => up.Perfil)
				.HasForeignKey(up => up.RoleId)
				.IsRequired();

			builder.HasMany(u => u.Permissoes)
				.WithOne(up => up.Perfil)
				.HasForeignKey(up => up.RoleId)
				.IsRequired();
		}
	}
}
