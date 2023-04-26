using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Aspti.Infra.CrossCutting.LogAudit.Model;

namespace Aspti.Infra.CrossCutting.LogAudit
{
    public interface ILogAuditoriaDbContext
    {
        DbSet<Auditoria> Auditoria { get; set; }
        DbSet<AuditoriaPropriedade> AuditoriaPropriedade { get; set; }
        ChangeTracker ChangeTracker { get; }
    }
}
