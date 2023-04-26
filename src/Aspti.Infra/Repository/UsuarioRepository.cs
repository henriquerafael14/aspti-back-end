using Aspti.Domain.Entidades;
using Aspti.Domain.Interface.Repository;
using Aspti.Infra.Data.Context;
using Aspti.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aspti.Infra.Data.Repository
{
	public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AsptiDbContext context)
            : base(context)
        {
        }

        public override void Atualizar(Usuario obj)
        {
            var objContext = Db.Set<Usuario>().Local.FirstOrDefault(x => x.Id == obj.Id);
            if (objContext != null)
                Db.Entry(objContext).State = EntityState.Detached;
            base.Atualizar(obj);
        }

        public async Task<bool> EmailJaCadastrado(string email)
        {
            return await Db.Users.AnyAsync(x => x.Email.ToUpper() == email.ToUpper());
        }

        public async Task<List<PerfilPermissao>> ObterPerfilPermissoes(Guid usuarioId)
        {
            return await Db.UserRoles
                .Include(x => x.Perfil).ThenInclude(x => x.Permissoes)
                .Where(x => x.UserId == usuarioId)
                .SelectMany(x => x.Perfil.Permissoes).ToListAsync();
        }

        public async Task<Usuario> ObterPorEmail(string email)
            => await ObterTodos().AsNoTracking().
            FirstOrDefaultAsync(x => x.Email == email);

        public async Task<Usuario> ObterUsuarioComPerfis(Guid usuarioId)
        {
            return await Db.Users
                .Include(u => u.UsuarioPerfis)
                .FirstOrDefaultAsync(u => u.Id == usuarioId);
        }
    }
}
