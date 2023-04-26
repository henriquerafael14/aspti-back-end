using Aspti.Domain.Entidades;
using System;
using System.Collections.Generic;

namespace Aspti.Domain.Interface.Repository
{
	public interface ITelaRepository : IRepository<Tela>
	{
		IEnumerable<Tela> ObterPorCategoriaId(Guid categoriaId);
		IEnumerable<Tela> ObterPorNome(string nome);
	}

}
