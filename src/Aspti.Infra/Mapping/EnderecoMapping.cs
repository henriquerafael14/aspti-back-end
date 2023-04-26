using Aspti.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Aspti.Infra.Data.Mapping
{
	public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
	{
		public void Configure(EntityTypeBuilder<Endereco> builder)
		{
			builder.ToTable(Constantes.Constantes.Tabelas.Endereco, Constantes.Constantes.Schemas.Sistema);

			builder.Property(endereco => endereco.CEP).HasMaxLength(8);
			builder.Property(endereco => endereco.Logradouro).HasMaxLength(200);
			builder.Property(endereco => endereco.Complemento).HasMaxLength(200);
			builder.Property(endereco => endereco.Bairro).HasMaxLength(200);
			builder.Property(endereco => endereco.Numero).HasMaxLength(50);
			builder.Property(endereco => endereco.CidadeNome).HasMaxLength(100);
			builder.Property(endereco => endereco.EstadoNome).HasMaxLength(50);

		}
	}
}
