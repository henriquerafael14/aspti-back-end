using Aspti.Domain.Entidades;
using System;
using System.Collections.Generic;

namespace Aspti.Domain.Interface.Service
{
	public interface ITelaService : IService<Tela>
	{
		IEnumerable<Tela> ObterPorCategoriaId(Guid categoriaId);
		Tela ObterPorNome(string nome);
	}
}
