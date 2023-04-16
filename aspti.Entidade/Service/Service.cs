using Aspti.Domain.Interface.Repository;
using Aspti.Domain.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aspti.Domain.Service
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;

        public Service(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual TEntity Adicionar(TEntity obj)
        {
            return _repository.Adicionar(obj);
        }

        public virtual TEntity ObterPorId(int id)
        {
            return _repository.ObterPorId(id);
        }

        public virtual TEntity ObterPorId(Guid id)
        {
            return _repository.ObterPorId(id);
        }

        public virtual async Task<TEntity> ObterPorIdAsync(Guid id)
        {
            return await _repository.ObterPorIdAsync(id);
        }

        public virtual IQueryable<TEntity> ObterTodos()
        {
            return _repository.ObterTodos();
        }

        public virtual async Task<IEnumerable<TEntity>> ObterTodosAsync()
        {
            return await _repository.ObterTodosAsync();
        }

        public virtual IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Buscar(predicate);
        }

        public virtual void Atualizar(TEntity obj)
        {
            _repository.Atualizar(obj);
        }

        public virtual void Remover(int id)
        {
            _repository.Remover(id);
        }

        public virtual void Remover(Guid id)
        {
            _repository.Remover(id);
        }

        public virtual void Remover(TEntity entity)
        {
            _repository.Remover(entity);
        }

        public virtual bool PossuiRegistros()
            => ObterTodos().Any();

        public void AdicionarVarios(List<TEntity> listaObjs)
        {
            _repository.AdicionarVarios(listaObjs);
        }

        public void Dispose()
        {
            _repository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}