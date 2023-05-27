using Aspti.Infra.CrossCutting.Enums;

namespace Aspti.Infra.CrossCutting.Filters.Attributes
{
	public class ClaimsAuthorizeAttribute : Attribute
	{
		public PermissoesEnum Permissoes { get; set; }
	}
}
