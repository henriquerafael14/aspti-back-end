using Aspti.Application.AppService.Interface;
using Aspti.Application.Request;
using Aspti.Application.Response;
using Aspti.Infra.CrossCutting.Autenticacao.Extensions;
using Aspti.Infra.CrossCutting.Constantes;
using Aspti.Infra.CrossCutting.Notificacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;

namespace Aspti.API.Controllers
{
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class UsuarioController : BaseController
	{
		private readonly IUsuarioAppService _usuarioAppService;

		public UsuarioController(IUsuarioAppService usuarioAppService,
		INotificador notificador,
		IUsuarioLogado usuarioLogado,
		ILogger<UsuarioController> logger) : base(notificador, usuarioLogado, logger)
		{
			_usuarioAppService = usuarioAppService;
		}

		//[HttpPost, AllowAnonymous]
		//[SwaggerResponse((int)HttpStatusCode.OK, "", typeof(Response<RegistroUsuarioResponse>))]
		//[SwaggerOperation(Summary = ConstantesSistema.DescricaoUsuario.Registrar)]
		//public async Task<IActionResult> Registrar([FromBody] UsuarioRequest registrarUsuarioRequest)
		//{
		//	var usuario = await _usuarioAppService.RegistrarUsuario(registrarUsuarioRequest);
		//	return CustomResponse(usuario);
		//}
	}
}
