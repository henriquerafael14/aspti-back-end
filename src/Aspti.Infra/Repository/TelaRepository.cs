using Aspti.Domain.Entidades;
using Aspti.Domain.Interface.Repository;
using Aspti.Infra.Data.Context;
using Aspti.Infra.Repository;
using System;
using System.Collections.Generic;

namespace Aspti.Infra.Data.Repository
{
	public class TelaRepository : Repository<Tela>, ITelaRepository
	{
		public TelaRepository(AsptiDbContext context)
			: base(context)
		{
		}

		public IEnumerable<Tela> ObterPorCategoriaId(Guid categoriaId)
			=> Buscar(x => x.CategoriaTelaId == categoriaId);

		public IEnumerable<Tela> ObterPorNome(string nome)
			=> Buscar(x => x.Nome == nome);


	}
}
