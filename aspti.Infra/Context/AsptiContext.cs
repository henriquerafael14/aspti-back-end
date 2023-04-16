using Aspti.Domain.Entidades;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Aspti.Infra.CrossCutting.LogAudit;
using Aspti.Infra.CrossCutting.LogAudit.Model;
using System.Reflection;
using System.Linq;

namespace Aspti.Infra.Context
{
	public class AsptiContext :
		IdentityDbContext<Usuario, Perfil, Guid, UsuarioPermissao, UsuarioPerfil, UsuarioLogin, PerfilPermissao,
		UsuarioToken>, ILogAuditoriaDbContext
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IConfiguration _configuration;

        public AsptiContext()
        {
        }

        public AsptiContext(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<PerfilPermissao> PerfilPermissao { get; set; }
        public DbSet<UsuarioLogin> UsuarioLogin { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<UsuarioPerfil> UsuarioPerfil { get; set; }
        public DbSet<UsuarioPermissao> UsuarioPermissao { get; set; }
        public DbSet<UsuarioToken> UsuarioToken { get; set; }

        public DbSet<Auditoria> Auditoria { get; set; }
        public DbSet<AuditoriaPropriedade> AuditoriaPropriedade { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            DefineTiposPadrao(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private static void DefineTiposPadrao(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
                                .SelectMany(t => t.GetProperties())
                                .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?)))
            {
                property.SetColumnType("timestamp without time zone");
            }
        }
    }
}
