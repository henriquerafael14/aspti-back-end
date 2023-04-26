using Aspti.Domain.Interface.Repository;
using Aspti.Infra.Data.Context;
using Aspti.Infra.CrossCutting.LogAudit.Model;
using Aspti.Infra.Repository;

namespace Aspti.Infra.Data.Repository
{
	public class AuditoriaRepository : Repository<Auditoria>, IAuditoriaRepository
	{
		public AuditoriaRepository(AsptiDbContext context)
			: base(context)
		{
		}
	}
}
