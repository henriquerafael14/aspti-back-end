using Aspti.Domain.Entidades;
using Aspti.Infra.CrossCutting.Paginacao;
using System.Threading.Tasks;

namespace Aspti.Domain.Interface.Repository
{
	public interface IPerfilRepository : IRepository<Perfil>
	{
		Task<ResultPaginado<Perfil>> ObterTodosComPermissoes(InputPaginado inputPaginado);
	}
}
