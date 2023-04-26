using Aspti.Application.AppService;
using Aspti.Application.AppService.Interface;
using Aspti.Application.AutoMapper;
using Aspti.Domain.Entidades;
using Aspti.Domain.Interface.Repository;
using Aspti.Domain.Interface.Service;
using Aspti.Domain.Service;
using Aspti.Infra.Data.Context;
using Aspti.Infra.CrossCutting.Autenticacao.Extensions;
using Aspti.Infra.CrossCutting.Notificacoes;
using Aspti.Infra.CrossCutting.UoW;
using Aspti.Infra.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aspti.Infra.CrossCutting.IoC
{
	public static partial class BootStrapperApi
	{
		public static void RegisterServicesApi(this IServiceCollection services)
		{
			services.AddHttpContextAccessor();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			//services.AddSingleton<IStringLocalizer, StringLocalizer<idioma>>();
			services.AddScoped<INotificador, Notificador>();
			services.AddScoped<IUsuarioLogado, UsuarioLogado>();

			services.AddScoped<IUnitOfWork, UnitOfWork>();

			RegisterRepositories(services);
			RegisterServices(services);
			RegisterAppServices(services);
			RegisterIdentityDependencies(services);
			RegisterHttpClients(services);
			
			new AutoMapperConfig(services);
		}

		public static void RegisterApiDatabase(this IServiceCollection services, string connectionString)
		{
			services.AddDbContext<AsptiDbContext>(options =>
				options.UseNpgsql(connectionString).UseLazyLoadingProxies(false));
			services.AddScoped<ConfigurationsDbContext>();
		}

		private static void RegisterIdentityDependencies(IServiceCollection services)
		{
			services.AddScoped<IUsuarioService, UsuarioService>();
			services.AddScoped<UserManager<Usuario>>();
			services.AddScoped<RoleManager<Perfil>>();
			services.AddScoped<SignInManager<Usuario>>();
		}

		private static void RegisterRepositories(IServiceCollection services)
		{
			services.AddScoped<IUsuarioRepository, UsuarioRepository>();
		}

		private static void RegisterServices(IServiceCollection services)
		{
			services.AddScoped<IUsuarioService, UsuarioService>();
		}

		private static void RegisterAppServices(IServiceCollection services)
		{
			services.AddScoped<IUsuarioAppService, UsuarioAppService>();
		}

		private static void RegisterHttpClients(IServiceCollection services)
		{
			//services.AddHttpClient<ICEPService, CEPService>();
		}
	}

}
