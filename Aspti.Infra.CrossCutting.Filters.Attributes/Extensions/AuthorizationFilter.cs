using Aspti.Infra.CrossCutting.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text;
using Aspti.Infra.CrossCutting.Extensions;

namespace Aspti.Infra.CrossCutting.Filters.Extensions
{
	public class AuthorizationFilter : IAuthorizationFilter
	{
		private const string BLACKLIST = "blacklist";

		private readonly PermissaoClaimNameEnum _claimName;
		private readonly PermissaoClaimValueEnum _claimValue;

		public AuthorizationFilter(PermissaoClaimNameEnum claimName,
								   PermissaoClaimValueEnum claimValue)
		{
			_claimName = claimName;
			_claimValue = claimValue;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (!context.HttpContext.User.Identity.IsAuthenticated || !ValidarAutenticacao(context.HttpContext))
			{
				context.Result = new StatusCodeResult((int)HttpStatusCode.Unauthorized);
				return;
			}

			if (!ValidarClaims(context.HttpContext, _claimName, _claimValue))
			{
				context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
			}
		}

		private bool ValidarAutenticacao(HttpContext context)
		{
			return context.User.Identity.IsAuthenticated && EhTokenValido(context);
		}

		public static bool ValidarClaims(HttpContext context, PermissaoClaimNameEnum claimName, PermissaoClaimValueEnum claimValue)
		{
			return context.User.IsInRole(PerfilPadraoEnum.Admin.GetDescription())
				|| context.User.Claims.Any(c => c.Type == claimName.ToString() && c.Value.Contains(claimValue.GetDescription()));
		}

		private static bool EhTokenValido(HttpContext context)
		{
			var token = context.ObterBearerToken();

			var key = $"{BLACKLIST}{token}";

			var distributedCache = context.ObterService<IDistributedCache>();
			var blackListPossuiToken = distributedCache.GetString(key) is not null;

			return !blackListPossuiToken;
		}
	}

}
