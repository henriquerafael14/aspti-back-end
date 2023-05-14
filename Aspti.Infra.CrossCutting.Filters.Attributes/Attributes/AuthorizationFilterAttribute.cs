using Aspti.Infra.CrossCutting.Enums;
using Aspti.Infra.CrossCutting.Filters.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Aspti.Infra.CrossCutting.Filters.Attributes
{
	public class AuthorizationFilterAttribute : TypeFilterAttribute
	{
		public AuthorizationFilterAttribute(PermissaoClaimNameEnum claimName, PermissaoClaimValueEnum claimValue) : base(typeof(AuthorizationFilter))
		{
			Arguments = new object[] { claimName, claimValue };
		}
	}
}
