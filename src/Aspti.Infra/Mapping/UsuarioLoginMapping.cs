using Aspti.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aspti.Infra.Data.Mapping
{
	public class UsuarioLoginMapping : IEntityTypeConfiguration<UsuarioLogin>
	{
		public void Configure(EntityTypeBuilder<UsuarioLogin> builder)
		{
			builder.ToTable(Constantes.Constantes.Tabelas.UsuarioLogin, Constantes.Constantes.Schemas.Identidade);
		}
	}
}
