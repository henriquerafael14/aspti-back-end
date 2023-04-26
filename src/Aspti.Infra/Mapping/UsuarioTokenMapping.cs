using Aspti.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aspti.Infra.Data.Mapping
{
	public class UsuarioTokenMapping : IEntityTypeConfiguration<UsuarioToken>
	{
		public void Configure(EntityTypeBuilder<UsuarioToken> builder)
		{
			builder.ToTable(Constantes.Constantes.Tabelas.UsuarioToken, Constantes.Constantes.Schemas.Identidade);
		}
	}
}
