using Aspti.Infra.CrossCutting.Enums;
using System;
using System.Collections.Generic;

namespace Aspti.Infra.CrossCutting.LogAudit.Model
{
	public class Auditoria
    {
        public Auditoria()
        {
            Propriedades = new List<AuditoriaPropriedade>();
        }

        public Guid Id { get; set; }
        public DateTime DataAuditoria { get; set; }
        public TipoAuditoriaEnum TipoAuditoria { get; set; }
        public string IdEntidade { get; set; }
        public string NomeEntidade { get; set; }
        public string NomeTabela { get; set; }
        public virtual ICollection<AuditoriaPropriedade> Propriedades { get; set; }
        public Guid? UsuarioId { get; set; }
        public string Usuario { get; set; }
    }
}
