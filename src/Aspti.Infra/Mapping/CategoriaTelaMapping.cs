using Aspti.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aspti.Infra.Data.Mapping
{
	public class CategoriaTelaMapping : IEntityTypeConfiguration<CategoriaTela>
	{
		public void Configure(EntityTypeBuilder<CategoriaTela> builder)
		{
			builder.ToTable(Constantes.Constantes.Tabelas.CategoriaTela, Constantes.Constantes.Schemas.Sistema);
		}
	}
}
