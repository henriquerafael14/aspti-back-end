using Aspti.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aspti.Infra.Data.Mapping
{
	public class UsuarioPermissaoMapping : IEntityTypeConfiguration<UsuarioPermissao>
	{
		public void Configure(EntityTypeBuilder<UsuarioPermissao> builder)
		{
			builder.ToTable(Constantes.Constantes.Tabelas.UsuarioPermissao, Constantes.Constantes.Schemas.Identidade);
		}
	}

}
