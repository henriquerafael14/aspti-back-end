using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aspti.Domain.Interface.Service
{
    public interface IService<TEntity> : IDisposable where TEntity : class
    {
        TEntity Adicionar(TEntity obj);
        TEntity ObterPorId(int id);
        TEntity ObterPorId(Guid id);
        Task<TEntity> ObterPorIdAsync(Guid id);
        IQueryable<TEntity> ObterTodos();
        Task<IEnumerable<TEntity>> ObterTodosAsync();
        IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate);
        void Atualizar(TEntity obj);
        void Remover(int id);
        void Remover(TEntity entity);
        void Remover(Guid id);
        bool PossuiRegistros();
        void AdicionarVarios(List<TEntity> listaObjs);
    }
}
