using Aspti.Domain.Entidades;
using Aspti.Domain.Interface.Repository;
using Aspti.Infra.Data.Context;
using Aspti.Infra.CrossCutting.Paginacao;
using Aspti.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Aspti.Infra.Data.Repository
{
	public class PerfilRepository : Repository<Perfil>, IPerfilRepository
	{
		public PerfilRepository(AsptiDbContext context)
			: base(context)
		{
		}

		public override void Atualizar(Perfil obj)
		{
			var objContext = Db.Set<Perfil>().Local
				.FirstOrDefault(x => x.Id == obj.Id);
			if (objContext != null)
				Db.Entry(objContext).State = EntityState.Detached;
			base.Atualizar(obj);
		}

		public async Task<ResultPaginado<Perfil>> ObterTodosComPermissoes(InputPaginado input)
		{
			return await Db.Set<Perfil>()
				.Include(p => p.Permissoes)
				.AsNoTracking()
				.PaginarAsync(input);
		}
	}
}
