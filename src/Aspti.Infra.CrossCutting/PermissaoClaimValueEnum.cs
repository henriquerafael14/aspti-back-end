using System.ComponentModel;

namespace Aspti.Infra.CrossCutting.Enums
{
	public enum PermissaoClaimValueEnum
    {
        [Description("C")]
        Criar,
        [Description("R")]
        Visualizar,
        [Description("U")]
        Atualizar,
        [Description("D")]
        Excluir
    }
}
