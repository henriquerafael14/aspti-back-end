using Aspti.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aspti.Infra.Data.Mapping
{
	public class PerfilPermissaoMapping : IEntityTypeConfiguration<PerfilPermissao>
	{
		public void Configure(EntityTypeBuilder<PerfilPermissao> builder)
		{
			builder.ToTable(Constantes.Constantes.Tabelas.PerfilPermissao, Constantes.Constantes.Schemas.Identidade);
		}
	}

}
