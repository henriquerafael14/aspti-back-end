//using Microsoft.EntityFrameworkCore;
using Aspti.Domain.Interface.Repository;
using Aspti.Infra.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aspti.Infra.Repository
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		protected AsptiContext Db;
		protected Microsoft.EntityFrameworkCore.DbSet<TEntity> DbSet;

		public Repository(AsptiContext context)
		{
			Db = context;
			DbSet = Db.Set<TEntity>();
		}

		public virtual TEntity Adicionar(TEntity obj)
		{
			var objreturn = DbSet.Add(obj);
			return objreturn.Entity;
		}

		public virtual TEntity ObterPorId(int id)
		{
			return DbSet.Find(id);
		}

		public virtual TEntity ObterPorId(Guid id)
		{
			return DbSet.Find(id);
		}

		public virtual async Task<TEntity> ObterPorIdAsync(Guid id)
		{
			return await DbSet.FindAsync(id);
		}

		public virtual IQueryable<TEntity> ObterTodos()
		{
			return DbSet;
		}

		public async Task<IEnumerable<TEntity>> ObterTodosAsync()
		{
			return await DbSet.AsNoTracking().ToListAsync();
		}

		public virtual void Atualizar(TEntity obj)
		{
			Db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

			DbSet.Update(obj);
		}

		public virtual void Remover(int id)
		{
			DbSet.Remove(DbSet.Find(id));
		}

		public virtual void Remover(TEntity entity)
		{
			if (Db.Entry(entity).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
				DbSet.Attach(entity);

			DbSet.Remove(entity);
		}

		public virtual void Remover(Guid id)
		{
			DbSet.Remove(DbSet.Find(id));
		}

		public IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
		{
			return DbSet.AsNoTracking().Where(predicate);
		}

		public void AdicionarVarios(List<TEntity> listaObjs)
		{
			DbSet.AddRange(listaObjs);
		}

		public void Dispose()
		{
			Db.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
