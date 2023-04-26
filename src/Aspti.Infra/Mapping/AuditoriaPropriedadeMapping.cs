using Aspti.Infra.CrossCutting.LogAudit.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aspti.Infra.Data.Mapping
{
	public class AuditoriaPropriedadeMapping : IEntityTypeConfiguration<AuditoriaPropriedade>
	{
		public void Configure(EntityTypeBuilder<AuditoriaPropriedade> builder)
		{
			builder.ToTable(Constantes.Constantes.Tabelas.AuditoriaPropriedade, Constantes.Constantes.Schemas.Sistema);
		}
	}
}
