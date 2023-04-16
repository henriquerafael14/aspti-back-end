using System;

namespace Aspti.Infra.CrossCutting.LogAudit.Model
{
	public class AuditoriaPropriedade
    {
        public AuditoriaPropriedade()
        {
            Id = Guid.NewGuid();
        }

        public AuditoriaPropriedade(Guid auditoriaId, string nomeDaPropriedade, string valorAntigo, string valorNovo, bool chavePrimaria)
        {
            Id = Guid.NewGuid();
            AuditoriaId = auditoriaId;
            NomeDaPropriedade = nomeDaPropriedade;
            ValorAntigo = valorAntigo;
            ValorNovo = valorNovo;
            ChavePrimaria = chavePrimaria;
        }

        public Guid Id { get; set; }
        public string NomeDaPropriedade { get; set; }
        public string ValorAntigo { get; set; }
        public string ValorNovo { get; set; }
        public bool ChavePrimaria { get; set; }

        public Guid AuditoriaId { get; set; }
        public virtual Auditoria Auditoria { get; set; }
    }

}
