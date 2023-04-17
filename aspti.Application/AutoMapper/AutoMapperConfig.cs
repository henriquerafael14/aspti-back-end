using Aspti.Application.Response;
using Aspti.Domain.Entidades;
using Aspti.Infra.ServicosExternos.BuscaCep.Response;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspti.Application.AutoMapper
{
	public class AutoMapperConfig
	{
		public AutoMapperConfig(IServiceCollection services)
		{
			var mapperConfig = new MapperConfiguration(config =>
			{
				ConfigureMappingsApi(config);
			});

			var mapper = mapperConfig.CreateMapper();
			services.AddSingleton(services);
		}

		private void ConfigureMappingsApi(IMapperConfigurationExpression config)
		{
			config.CreateMap<Usuario, UsuarioResponse>()
				.ForMember(dest => dest.Telefone, opt => opt.MapFrom(src => src.PhoneNumber))
				.ForMember(dest => dest.Perfis, opt => opt.MapFrom(src => src.UsuarioPerfis.Select(x => x.Perfil)));

			config.CreateMap<JsonViaCep, BuscarCepResponse>();
			config.CreateMap<JsonBrasilApi, BuscarCepResponse>();
		}
	}
}
