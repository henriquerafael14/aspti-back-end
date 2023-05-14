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
	public class AutenticacaoController : BaseController
	{
		private readonly IUsuarioAppService _usuarioAppService;

		public AutenticacaoController(IUsuarioAppService usuarioAppService,
			INotificador notificador,
			IUsuarioLogado usuarioLogado,
			ILogger<AutenticacaoController> logger) : base(notificador, usuarioLogado, logger)
		{
			_usuarioAppService = usuarioAppService;
		}

		#region Autenticação
		[HttpPost("autenticar"), AllowAnonymous]
		[SwaggerResponse((int)HttpStatusCode.OK, "", typeof(Response<LoginResponse>))]
		[SwaggerOperation(Summary = ConstantesSistema.DescricaoAutenticacao.Autenticar)]
		public async Task<IActionResult> Autenticar([FromBody] LoginRequest login)
		{
			var loginResponse = await _usuarioAppService.RealizarLogin(login);
			return CustomResponse(loginResponse);
		}

		[HttpPut("invalidar-token")]
		[SwaggerResponse((int)HttpStatusCode.OK, "", typeof(Response<bool>))]
		[SwaggerOperation(Summary = ConstantesSistema.DescricaoAutenticacao.InvalidarToken)]
		public IActionResult InvalidarToken()
		{
			_usuarioAppService.InvalidarToken();
			return CustomResponse(true);
		}

		[HttpGet("token-valido")]
		[SwaggerResponse((int)HttpStatusCode.OK, "", typeof(Response<TokenValidoResponse>))]
		[SwaggerOperation(Summary = ConstantesSistema.DescricaoAutenticacao.TokenValido)]
		public IActionResult TokenValido()
		{
			var tokenValidoResponse = _usuarioAppService.EhTokenValido();
			return CustomResponse(tokenValidoResponse);
		}
		#endregion
	}
}
