using Aspti.Infra.CrossCutting.Autenticacao.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Net;
using System.Threading.Tasks;
using System;

namespace Aspti.API.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;

		public ExceptionMiddleware(
			RequestDelegate next,
			ILogger<ExceptionMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext httpContext, IUsuarioLogado usuarioLogado)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

				LogarExcessao(ex, usuarioLogado.ObterId());
			}
		}

		public void LogarExcessao(Exception ex, string usuarioId)
		{
			LogContext.PushProperty("UsuarioId", usuarioId?.ToString());
			_logger.LogError(ex, ex.Message);
		}
	}

}
