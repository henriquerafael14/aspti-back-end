using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Aspti.API.Configuration
{
	public static class SwaggerConfiguration
	{
		public static void AddSwaggerConfigurations(this IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.EnableAnnotations();
				options.SchemaFilter<EnumSchemaFilter>();
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Insira JWT com portador no campo, exemplo: Bearer {seu token}",
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "bearer",
				});
				options.AddSecurityRequirement(new OpenApiSecurityRequirement {
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					new string[] { }
				}
			});
			});
		}

		public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
		{
			app.UseSwagger();
			app.UseSwaggerUI(
				options =>
				{
					foreach (var description in provider.ApiVersionDescriptions)
					{
						options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
					}
				});
			return app;
		}
	}

}
