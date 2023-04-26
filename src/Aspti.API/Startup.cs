using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using Aspti.API.Configuration;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Aspti.Infra.Data.Context;
using Aspti.API.Middlewares;
using Aspti.Infra.CrossCutting.IoC;
using StackExchange.Redis;
using Newtonsoft.Json.Converters;

namespace Aspti.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
				.Enrich.FromLogContext()
				.CreateLogger();

			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.RegisterServicesApi();
			services.RegisterApiDatabase(Configuration.GetConnectionString("DefaultConnection"));
			
			services.AddLogging(logging => logging.AddSerilog());

			services.AddCors();

			services.AddApiVersioning(options =>
			{
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.DefaultApiVersion = new ApiVersion(1, 0);
				options.ReportApiVersions = true;
			});

			services.AddVersionedApiExplorer(options =>
			{
				options.GroupNameFormat = "'v'VVV";
				options.SubstituteApiVersionInUrl = true;
			});

			services.AddControllers().AddNewtonsoftJson(opt =>
			{
				opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
				opt.SerializerSettings.Converters.Add(new StringEnumConverter());
			});

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.SuppressModelStateInvalidFilter = true;
			});

			services.AddSwaggerConfigurations();
			services.AddIdentityConfiguration();
			services.AddJwtAuthentication();

			var redisConfigurationOptions = ConfigurationOptions.Parse(Configuration["Redis:redisConfigurationOptions"]);

			services.AddStackExchangeRedisCache(redisCacheConfig =>
			{
				redisCacheConfig.ConfigurationOptions = redisConfigurationOptions;
			});

			services.AddSession(options =>
			{
				options.Cookie.Name = "pb_session";
				options.IdleTimeout = TimeSpan.FromMinutes(60 * 24);
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider,
			ConfigurationsDbContext dbMigrationsConfig)
		{
			if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

			dbMigrationsConfig.Migrate().Wait();

			dbMigrationsConfig.SeedData().Wait();

			app.UseSession();

			app.UseRouting();

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseCors(x => x
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());

			app.UseSwaggerConfiguration(provider);

			app.UseMiddleware<ExceptionMiddleware>();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}
