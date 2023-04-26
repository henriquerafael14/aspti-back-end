using Aspti.Domain.Entidades;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aspti.Domain.Interface.Service
{
	public interface IUsuarioService : IService<Usuario>
	{
		//Task<Usuario> ObterPorEmail(string usuarioEmail);
		//void Inativar(Guid id);
		//Task<string> GerarTokenClaims(string email);
		//Task RegistrarUsuario(Usuario usuario, string senha);
		//bool EhTokenValido();
		//void InvalidarToken();
		//Task<IdentityResult> ResetSenha(Guid usuarioId, string token, string novaSenha);
		//Task<IdentityResult> AlterarSenha(string UsuarioId, string senhaAtual, string novaSenha);
		//Task AlterarPerfis(Guid usuarioId, List<Guid> perfisId);
		//Task<List<PerfilPermissao>> ObterPerfilPermissoes(Guid usuarioId);
		//Task<bool> EmailJaCadastrado(string email);
	}
}
