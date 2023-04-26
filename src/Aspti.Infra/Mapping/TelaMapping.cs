using Aspti.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aspti.Infra.Data.Mapping
{
	public class TelaMapping : IEntityTypeConfiguration<Tela>
	{
		public void Configure(EntityTypeBuilder<Tela> builder)
		{
			builder.ToTable(Constantes.Constantes.Tabelas.Tela, Constantes.Constantes.Schemas.Sistema);
			builder.Property(e => e.Status).HasConversion<string>().HasColumnType("varchar(24)");
			builder.HasOne(t => t.CategoriaTela)
				.WithMany(c => c.Telas)
				.HasForeignKey(t => t.CategoriaTelaId);
		}
	}
}
