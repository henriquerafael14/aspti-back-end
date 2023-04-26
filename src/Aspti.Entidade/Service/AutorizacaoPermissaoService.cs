using Aspti.Domain.Interface.Service;
using Aspti.Infra.CrossCutting.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Aspti.Domain.Service
{
	public class AutorizacaoPermissaoService : IAutorizacaoPermissaoService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IPerfilPermissaoService _perfilPermissaoService;
		private readonly IPerfilService _perfilService;
		private readonly ITelaService _telaService;
		private readonly IUsuarioPerfilService _usuarioPerfilService;

		public AutorizacaoPermissaoService(
			IUsuarioPerfilService usuarioPerfilAppService,
			IPerfilPermissaoService perfilPermissaoAppService,
			IPerfilService perfilService,
			ITelaService telaService,
			IHttpContextAccessor httpContextAccessor)
		{
			_usuarioPerfilService = usuarioPerfilAppService;
			_perfilPermissaoService = perfilPermissaoAppService;
			_perfilService = perfilService;
			_telaService = telaService;
			_httpContextAccessor = httpContextAccessor;
		}

		public bool Autorizar(string controller, PermissoesEnum permissao)
		{
			if (NaoExistemPerfisPermissao())
				return true;

			return TemPermissao(controller, permissao);
		}

		private bool TemPermissao(string controller, PermissoesEnum permissao)
		{
			var perfilUsuarioLogadoIds = _usuarioPerfilService
												.ObtemIdsPerfisUsuario(UsuarioLogadoId(_httpContextAccessor.HttpContext));

			if (PerfilAdministrador(perfilUsuarioLogadoIds))
				return true;

			var telaId = ObtemIdTela(controller);

			var permissoesUsuarioLogado = _perfilPermissaoService
						.PossuiPermissaoUsuarioLogado(perfilUsuarioLogadoIds, permissao, telaId);

			return permissoesUsuarioLogado;
		}

		private string ObtemIdTela(string controller)
		{
			if (controller == null)
				throw new ArgumentNullException(nameof(controller));

			var tela = _telaService
							.ObterPorNome(controller);

			if (tela == null)
				throw new ArgumentException(nameof(controller));

			return tela.Id.ToString();
		}

		private bool PerfilAdministrador(List<Guid> perfilUsuarioLogadoIds)
		{
			foreach (var perfil in perfilUsuarioLogadoIds)
				if (_perfilService.ObterPorId(perfil).Administrador)
					return true;
			return false;
		}

		private Guid UsuarioLogadoId(HttpContext httpContext)
		{
			return Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
		}

		private bool NaoExistemPerfisPermissao()
		{
			return !_usuarioPerfilService.PossuiRegistros() || !_perfilPermissaoService.PossuiRegistros();
		}
	}

}
