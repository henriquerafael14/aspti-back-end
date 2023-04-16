using Aspti.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aspti.Domain.Interface.Repository
{
	public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> ObterPorEmail(string email);
        Task<Usuario> ObterUsuarioComPerfis(Guid usuarioId);
        Task<List<PerfilPermissao>> ObterPerfilPermissoes(Guid usuarioId);
        Task<bool> EmailJaCadastrado(string email);
    }
}
