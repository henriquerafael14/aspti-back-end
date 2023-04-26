using Aspti.Infra.CrossCutting.Autenticacao.Extensions;
using Aspti.Infra.CrossCutting.Notificacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System;
using FluentValidation.Results;

namespace Aspti.API.Controllers
{
	[Authorize]
	[ApiController]
	public abstract class BaseController : Controller
	{
		private readonly INotificador _notificador;
		private readonly ILogger _logger;
		private string ActionName => ControllerContext.RouteData.Values["action"].ToString();
		private string ControllerName => ControllerContext.RouteData.Values["controller"].ToString();

		protected readonly IUsuarioLogado UsuarioLogado;

		protected BaseController(INotificador notificador,
								 IUsuarioLogado usuarioLogado,
								 ILogger logger)
		{
			_notificador = notificador;
			_logger = logger;

			UsuarioLogado = usuarioLogado;
		}

		#region Notificação

		protected bool OperacaoValida()
			=> !_notificador.TemNotificacao();

		protected List<string> ObterNotificacoes()
		{
			var mensagens = _notificador
				.ObterNotificacoes()
				.Select(n => n.Mensagem)
				.ToList();

			return mensagens;
		}

		protected void NotificarErro(string mensagem)
		{
			_notificador.AdicionarNotificacao(new Notificacao(mensagem));
		}

		protected ActionResult CustomResponse(object result = null)
		{
			if (OperacaoValida())
			{
				return Ok(new
				{
					success = true,
					data = result
				});
			}

			return BadRequest(new
			{
				success = false,
				errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
			});
		}


		protected ActionResult CustomError(string error)
		{
			NotificarErro(error);

			return CustomResponse();
		}

		protected ActionResult CustomError(Exception ex)
		{
			LogException(ex);

			NotificarErro("Erro interno, favor contatar o suporte!");

			return CustomResponse();
		}

		protected ActionResult CustomError(ValidationResult result)
		{
			if (result.IsValid) return Ok();

			var errors = result.Errors.Select(e => e.ErrorMessage);

			foreach (var error in errors) NotificarErro(error);

			return CustomResponse();
		}

		protected ActionResult CustomError(ModelStateDictionary modelState)
		{
			if (modelState.IsValid) return Ok();

			var propErrors = modelState
				.Keys
				.Where(k => modelState[k].Errors.Any())
				.Select(k => (Key: k, Error: modelState[k].Errors[0]));

			var errors = propErrors.Select(x =>
			{
				var (key, error) = x;

				var errorMessage = error.Exception == null
					? error.ErrorMessage
					: error.Exception.Message;

				return $"[{key}] - {errorMessage}";
			});

			foreach (var error in errors) NotificarErro(error);

			return CustomResponse();
		}

		#endregion

		#region Log

		protected void LogException(Exception ex)
		{
			_logger.LogError(ex, "{controllerName} - {actionName}", ControllerName, ActionName);
		}

		#endregion
	}
}
