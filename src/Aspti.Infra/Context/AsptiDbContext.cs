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
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;

namespace Aspti.Infra.Data.Context
{
	public class AsptiDbContext :
	IdentityDbContext<Usuario, Perfil, Guid, UsuarioPermissao, UsuarioPerfil, UsuarioLogin, PerfilPermissao,
		UsuarioToken>, ILogAuditoriaDbContext
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IConfiguration _configuration;

		public AsptiDbContext()
		{
		}

		public AsptiDbContext(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
		{
			_httpContextAccessor = httpContextAccessor;
			_configuration = configuration;
		}

		public DbSet<Tela> Tela { get; set; }
		public DbSet<CategoriaTela> CategoriaTela { get; set; }
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

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return Task.FromResult(SaveChanges());
		}

		public override int SaveChanges()
		{
			Guid? userId = null;
			if (!string.IsNullOrEmpty(_httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)
				?.Value))
				userId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
			new Startup(this).AddAuditLogs(userId, _httpContextAccessor?.HttpContext?.User?.Identity?.Name);
			return base.SaveChanges();
		}
	}

}
