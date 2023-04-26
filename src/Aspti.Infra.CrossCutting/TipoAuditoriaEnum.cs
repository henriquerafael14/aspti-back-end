using Microsoft.EntityFrameworkCore;

namespace Aspti.Infra.CrossCutting.Enums
{
    public enum TipoAuditoriaEnum
    {
        Excluido = EntityState.Deleted,
        Modificado = EntityState.Modified,
        Adicionado = EntityState.Added
    }
}
