using Aspti.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aspti.Infra.Data.Mapping
{
	public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
	{
		public void Configure(EntityTypeBuilder<Usuario> builder)
		{
			builder.ToTable(Constantes.Constantes.Tabelas.Usuario, Constantes.Constantes.Schemas.Identidade);

			builder.Property(usuario => usuario.Nome).HasMaxLength(250);
			builder.Property(usuario => usuario.Sobrenome).HasMaxLength(250);
			builder.Property(usuario => usuario.CPFCNPJ).HasMaxLength(18);
			builder.Property(e => e.Status)
				.HasMaxLength(24)
				.HasColumnType("varchar")
				.HasConversion<string>();

			builder.HasOne(usuario => usuario.Endereco);
			builder.HasMany(usuario => usuario.UsuarioPerfis);
		}
	}
}
