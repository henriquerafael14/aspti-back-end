using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Aspti.Domain.Interface.Service;
using Aspti.Domain.Entidades;
using Aspti.Infra.CrossCutting.Filters.Attributes;
using System.Reflection;
using Aspti.Infra.CrossCutting.Filters.Extensions;

namespace Aspti.Infra.CrossCutting.Filters
{
	public class ClaimsRequirementFilter : IAuthorizationFilter
	{
		private readonly IUsuarioPerfilService _usuarioPerfilAppService;
		private readonly IPerfilPermissaoService _perfilPermissaoAppService;
		private readonly IPerfilService _perfilService;
		private readonly ITelaService _telaService;

		public ClaimsRequirementFilter(IUsuarioPerfilService usuarioPerfilAppService, IPerfilPermissaoService perfilPermissaoAppService, IPerfilService perfilService, ITelaService telaService)
		{
			_usuarioPerfilAppService = usuarioPerfilAppService;
			_perfilPermissaoAppService = perfilPermissaoAppService;
			_perfilService = perfilService;
			_telaService = telaService;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var idioma = context.HttpContext?.Request?.Path.ToString().Substring(1, 5);
			if (context.ActionDescriptor.GetArea() == "Identity")
				return;

			bool anon = context.ActionDescriptor.EndpointMetadata.Any(en => en.GetType() == typeof(AllowAnonymousAttribute));
			if (anon)
				return;

			if (!context.HttpContext.User.Identity.IsAuthenticated && !(context.ActionDescriptor.GetArea() == "Identity"))
			{
				context.Result = new RedirectResult("/Identity/Account/Login?ReturnUrl=%2F" + idioma);
				return;
			}

			List<Guid> perfilUsuarioLogadoIds = ObtemIdsPerfisUsuarioLogado(context);

			if (TemPermissao(context, perfilUsuarioLogadoIds))
				return;
			else
				context.Result = new RedirectResult("/" + idioma + "/Home/PermissaoNegada");
		}

		private List<Guid> ObtemIdsPerfisUsuarioLogado(AuthorizationFilterContext context)
		{
			var perfilUsuarioLogado = _usuarioPerfilAppService.ObterTodos().Where(x => x.UserId == UsuarioLogadoId(context));

			List<Guid> perfilUsuarioLogadoIds = perfilUsuarioLogado.Select(x => x.RoleId).ToList();
			return perfilUsuarioLogadoIds;
		}

		private IQueryable<PerfilPermissao> PermissoesUsuarioLogado(List<Guid> perfilUsuarioLogadoIds)
		{
			return _perfilPermissaoAppService.ObterTodos().Where(x => perfilUsuarioLogadoIds.Contains(x.RoleId));
		}

		private string ObtemIdTela(string controller)
		{
			return _telaService.ObterTodos().Where(x => x.Nome == controller).FirstOrDefault().Id.ToString();
		}

		private bool TemPermissao(AuthorizationFilterContext context, List<Guid> perfilUsuarioLogadoIds)
		{
			if (NaoExistemPerfisPermissao())
				return true;

			if (PerfilAdministrador(perfilUsuarioLogadoIds))
				return true;

			ClaimsAuthorizeAttribute attributoPermissao = BuscaAttributoPermissao(context);
			if (attributoPermissao == null)
				return true;

			var permissoesUsuarioLogado = PermissoesUsuarioLogado(perfilUsuarioLogadoIds);

			string telaId = ObtemIdTela(context.ActionDescriptor.GetController());

			return permissoesUsuarioLogado.Where(x => x.ClaimValue == attributoPermissao.Permissoes.ToString() && x.ClaimType == telaId).Count() > 0;
		}

		private bool PerfilAdministrador(List<Guid> perfilUsuarioLogadoIds)
		{
			foreach (var perfil in perfilUsuarioLogadoIds)
				if (_perfilService.ObterPorId(perfil).Administrador)
					return true;
			return false;
		}

		private bool NaoExistemPerfisPermissao()
		{
			return _usuarioPerfilAppService.ObterTodos().Count() == 0 || _perfilPermissaoAppService.ObterTodos().Count() == 0;
		}

		private static Guid UsuarioLogadoId(AuthorizationFilterContext context)
		{
			return Guid.Parse(context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
		}

		private static ClaimsAuthorizeAttribute BuscaAttributoPermissao(AuthorizationFilterContext context)
		{
			ClaimsAuthorizeAttribute actionPermissionAttribute = null;

			var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
			if (controllerActionDescriptor != null)
			{
				actionPermissionAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttribute<ClaimsAuthorizeAttribute>();
			}

			return actionPermissionAttribute;
		}
	}
}