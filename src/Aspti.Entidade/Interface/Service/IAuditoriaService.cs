using Aspti.Domain.Interface.Repository;
using Aspti.Infra.CrossCutting.LogAudit.Model;
using Aspti.Infra.CrossCutting.Paginacao;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aspti.Domain.Interface.Service
{
	public interface IAuditoriaService : IService<Auditoria>
	{
		//Task<ResultPaginado<Auditoria>> Obter(FiltroAuditoriaDTO filtro);
		Task<List<string>> ObterEntidades();
		Task<List<string>> ObterCampos(string entidade);
	}

}
