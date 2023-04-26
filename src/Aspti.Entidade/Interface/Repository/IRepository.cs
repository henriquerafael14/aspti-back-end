using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aspti.Domain.Interface.Repository
{
	public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Adicionar(TEntity obj);
        TEntity ObterPorId(int id);
        TEntity ObterPorId(Guid id);
        Task<TEntity> ObterPorIdAsync(Guid id);
        IQueryable<TEntity> ObterTodos();
        Task<IEnumerable<TEntity>> ObterTodosAsync();
        void Atualizar(TEntity obj);
        void Remover(int id);
        void Remover(Guid id);
        void Remover(TEntity entity);
        IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate);
        void AdicionarVarios(List<TEntity> listaObjs);
    }
}
