using Aspti.Infra.CrossCutting.LogAudit.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aspti.Infra.Data.Mapping
{
	public class AuditoriaMapping : IEntityTypeConfiguration<Auditoria>
	{
		public void Configure(EntityTypeBuilder<Auditoria> builder)
		{
			builder.ToTable(Constantes.Constantes.Tabelas.Auditoria, Constantes.Constantes.Schemas.Sistema);
		}
	}
}
