using Aspti.Domain.Entidades;
using Aspti.Domain.Models;
using Aspti.Infra.CrossCutting.Paginacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspti.Domain.Interface.Service
{
	public interface IPerfilService : IService<Perfil>
	{
		PerfilModel Novo();
		PerfilModel Adicionar(PerfilModel obj);
		void Atualizar(PerfilModel obj);
		Task<ResultPaginado<Perfil>> ObterTodosComPermissoes(InputPaginado input);
		Perfil AtualizarPerfil(Guid id, Perfil perfilAtualizar);
		Task Delete(Guid id);
	}
}
