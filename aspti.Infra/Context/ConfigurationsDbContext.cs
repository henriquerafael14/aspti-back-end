using Aspti.Domain.Entidades;
using Aspti.Infra.CrossCutting.Enums;
using Aspti.Infra.CrossCutting.Utils;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Aspti.Infra.Context
{
	class ConfigurationsDbContext
    {
        private readonly AsptiContext _dbContext;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<Perfil> _roleManager;

        public ConfigurationsDbContext(AsptiContext dbContext, UserManager<Usuario> userManager, RoleManager<Perfil> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Migrate()
        {
            await _dbContext.Database.MigrateAsync();
            await SeedData();
        }

        public async Task SeedData()
        {
            if (await _dbContext.Usuario.AnyAsync())
                return;

            await SeedBaseInicial();
        }

        private async Task SeedBaseInicial()
        {
            await using var tran = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var usuarioAdmin = await SeedAdmin();
                var perfilAdmin = await SeedPerfilAdmin();

                usuarioAdmin.AtualizarPerfis(new List<Guid>() { perfilAdmin.Id });

                await _dbContext.SaveChangesAsync();

                await tran.CommitAsync();
            }
            catch (Exception)
            {
                await tran.RollbackAsync();
                throw;
            }
        }

        private async Task<Perfil> SeedPerfilAdmin()
        {
            var perfilAdministrador = new Perfil { Id = Guid.NewGuid(), Name = "Administrador", NormalizedName = "ADMINISTRADOR" };

            var permissoes = new List<PermissaoClaimValueEnum>()
            {
                 PermissaoClaimValueEnum.Criar,
                 PermissaoClaimValueEnum.Visualizar,
                 PermissaoClaimValueEnum.Atualizar,
                 PermissaoClaimValueEnum.Excluir
            };

            var perfilAdminPermissoes = new PerfilPermissao()
            {
                ClaimType = PermissaoClaimNameEnum.Usuario.ToString(),
                ClaimValue = string.Join(",", permissoes.Select(x => x.ObtenhaDescricao()))
            };

            var perfilPermissoes = new List<PerfilPermissao>() { perfilAdminPermissoes };
            var perfilAdmin = new Perfil(Guid.NewGuid(), "admin", perfilPermissoes, true);

            await _roleManager.CreateAsync(perfilAdmin);
            return perfilAdmin;
        }

        private async Task<Usuario> SeedAdmin()
        {
            //admin@lyncas
            var passwordHash = "AQAAAAEAACcQAAAAEItiyIgXhxeezQtcQAWUc1hnCWTGL1SpJ8GMmC+vhjREzs7AXfJ5PaHkzomH4qRsWg==";
            var securityStamp = "LDNIKTODTSNQRL4TSDD7H4MZE2LKSYX5";
            var concurrencyStamp = "babdeeb8-1542-487c-b689-8f00d1ebda17";

            var usuarioAdmin = new Usuario(Guid.NewGuid(), "admin", "Admin", "ASPTI", "ra.costa@catolicasc.edu.br",
                UserStatusEnum.Ativo, "37834399063", DateTime.Now, "47999999999", passwordHash, securityStamp, concurrencyStamp);

            await _userManager.CreateAsync(usuarioAdmin);

            return usuarioAdmin;
        }
    }
}

