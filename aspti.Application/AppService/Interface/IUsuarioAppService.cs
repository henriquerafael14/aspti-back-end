﻿using Aspti.Application.Request;
using Aspti.Application.Response;
using Aspti.Domain.Entidades;
using Aspti.Infra.CrossCutting.Paginacao;
using System;
using System.Threading.Tasks;

namespace Aspti.Application.AppService.Interface
{
	public interface IUsuarioAppService : IAppService<Usuario>
    {
        Task<Usuario> ObterPorEmail(string usuarioEmail);
        void Inativar(Guid id);

        #region Api
        Task<LoginResponse> RealizarLogin(LoginRequest loginRequest);
        Task<string> GerarToken(string email);
        Task<RegistroUsuarioResponse> RegistrarUsuario(UsuarioRequest registroRequest);
        Task<ResultPaginado<UsuarioResponse>> ObterTodosPaginado(InputPaginado input);
        Task<UsuarioResponse> Atualizar(AtualizarUsuarioRequest usuarioAtualizar);
        void InvalidarToken();
        TokenValidoResponse EhTokenValido();
        Task EnviarEmailResetSenha(string email);
        Task ResetSenha(ResetSenhaRequest resetSenhaRequest);
        Task AlterarSenha(AlterarSenhaRequest alterarSenhaRequest);
        Task<UsuariosDropdownResponse> ObterDropdown();
        Task<AssociarPerfisResponse> AssociarPerfis(AssociarPerfisUsuarioRequest request);

        #endregion

    }
}