using Aspti.Domain.Interface.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aspti.Application.AppService.Interface
{
	public interface IAppService<TEntity> where TEntity : class, IEntidadeBase
    {
        Task<TEntityResult> ObterPorIdAsync<TEntityResult>(Guid id);
        Task<IEnumerable<TEntityResult>> ObterTodosAsync<TEntityResult>();
        Task RemoverAsync(Guid id);
    }
}
