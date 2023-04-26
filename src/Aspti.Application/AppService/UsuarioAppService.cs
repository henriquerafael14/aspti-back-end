using Aspti.Application.AppService.Interface;
using Aspti.Application.Request;
using Aspti.Application.Response;
using Aspti.Domain.Entidades;
using Aspti.Domain.Interface.Service;
using Aspti.Infra.CrossCutting.Autenticacao.Extensions;
using Aspti.Infra.CrossCutting.Constantes;
using Aspti.Infra.CrossCutting.Notificacoes;
using Aspti.Infra.CrossCutting.Paginacao;
using Aspti.Infra.CrossCutting.UoW;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aspti.Application.AppService
{
	public class UsuarioAppService : AppService<Usuario>, IUsuarioAppService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUsuarioService _userService;

		//private readonly UserManager<Usuario> _userManager;
		//private readonly SignInManager<Usuario> _signInManager;
		private readonly INotificador _notificador;
		private readonly ILogger<UsuarioAppService> _logger;
		//private readonly IHttpContextAccessor _httpContextAccessor;

		public UsuarioAppService(
			IUsuarioService userService,
			//UserManager<Usuario> userManager,
			//SignInManager<Usuario> signInManager,
			IMapper mapper,
			IUnitOfWork unitOfWork,
			INotificador notificador
			)
			//ILogger<UsuarioAppService> logger,
			//IHttpContextAccessor httpContextAccessor)
			: base(mapper, unitOfWork, notificador, userService)
		{
			_userService = userService;
			//_userManager = userManager;
			//_signInManager = signInManager;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_notificador = notificador;
			//_logger = logger;
			//_httpContextAccessor = httpContextAccessor;
		}

		//public async Task<Usuario> ObterPorEmail(string usuarioEmail)
		//{
		//	return await _userService.ObterPorEmail(usuarioEmail);
		//}

		//public void Inativar(Guid id)
		//{
		//	_userService.Inativar(id);
		//	_unitOfWork.Commit();
		//}

		//public async Task<LoginResponse> RealizarLogin(LoginRequest loginRequest)
		//{
		//	if (loginRequest is null)
		//	{
		//		_notificador.AdicionarNotificacao("Os dados são inválidos.");
		//		return null;
		//	}

		//	if (!loginRequest.EhValido())
		//	{
		//		var resultadoValidacao = loginRequest.Validar();
		//		_notificador.AdicionarNotificacoes(resultadoValidacao.Errors);
		//		return null;
		//	}

		//	var usuario = await ObterUsuarioPorEmailOuUsername(loginRequest);

		//	if (usuario == null)
		//	{
		//		_notificador.AdicionarNotificacao("E-mail incorreto ou usuário inexistente");
		//		return null;
		//	}

		//	if (!usuario.EmailConfirmed)
		//	{
		//		_notificador.AdicionarNotificacao("Confirme seu e-mail para prosseguirmos no processo de efetivação do cadastro!");
		//		return null;
		//	}

		//	var result = await _signInManager.PasswordSignInAsync(usuario, loginRequest.Senha, true, lockoutOnFailure: false);

		//	if (!result.Succeeded)
		//	{
		//		_notificador.AdicionarNotificacao("Tentativa de login inválida.");

		//		_logger.LogError($"Erro na tentativa de login usuário {usuario.Email}");

		//		return null;
		//	}

		//	_logger.LogInformation($"Usuario {usuario.Email} realizou login com sucesso.");

		//	var loginResponse = _mapper.Map<LoginResponse>(usuario);
		//	loginResponse.AccessToken = await GerarToken(usuario.Email);

		//	return loginResponse;
		//}

		//public async Task<string> GerarToken(string email)
		//{
		//	return await _userService.GerarTokenClaims(email);
		//}

		//public async Task<RegistroUsuarioResponse> RegistrarUsuario(UsuarioRequest registroRequest)
		//{
		//	if (registroRequest is null)
		//	{
		//		_notificador.AdicionarNotificacao("Os dados são inválidos.");
		//		return null;
		//	}

		//	if (!registroRequest.EhValido())
		//	{
		//		var resultadoValidacao = registroRequest.Validar();
		//		_notificador.AdicionarNotificacoes(resultadoValidacao.Errors);
		//		return null;
		//	}

		//	var emailJaCadastrado = await _userService.EmailJaCadastrado(registroRequest.Email);

		//	if (emailJaCadastrado)
		//	{
		//		_notificador.AdicionarNotificacao(MensagemValidacao.EmailJaCadastrado);
		//		return null;
		//	}

		//	var usuario = MapeiaUsuarioRequestParaUsuario(registroRequest);

		//	await _userService.RegistrarUsuario(usuario, registroRequest.Senha);

		//	_unitOfWork.Commit();

		//	var usuarioResponse = await _userManager.FindByEmailAsync(registroRequest.Email);
		//	return _mapper.Map<RegistroUsuarioResponse>(usuarioResponse);
		//}

		//public TokenValidoResponse EhTokenValido()
		//{
		//	var ehTokenValido = _userService.EhTokenValido();
		//	return new TokenValidoResponse()
		//	{
		//		Valido = ehTokenValido
		//	};
		//}

		//public void InvalidarToken()
		//{
		//	_userService.InvalidarToken();
		//}

		//public async Task ResetSenha(ResetSenhaRequest resetSenhaRequest)
		//{
		//	try
		//	{
		//		var result = await _userService.ResetSenha(resetSenhaRequest.UsuarioId, resetSenhaRequest.Token, resetSenhaRequest.NovaSenha);

		//		if (!result.Succeeded)
		//		{
		//			foreach (var erro in result.Errors)
		//			{
		//				_notificador.AdicionarNotificacao(erro.Description);
		//			}
		//		}
		//	}

		//	catch
		//	{
		//		_notificador.AdicionarNotificacao("Token inválido.");
		//	}
		//}

		//public async Task AlterarSenha(AlterarSenhaRequest alterarSenhaRequest)
		//{
		//	try
		//	{
		//		var usuarioId = _httpContextAccessor.HttpContext.User.GetUserId();

		//		if (usuarioId is null)
		//		{
		//			_notificador.AdicionarNotificacao("É necessário estar autenticado para alterar a senha.");
		//			return;
		//		}

		//		if (alterarSenhaRequest.NovaSenha != alterarSenhaRequest.NovaSenhaConfirmacao)
		//		{
		//			_notificador.AdicionarNotificacao("A nova senha e a confirmação de nova senha não coincidem.");
		//			return;
		//		}

		//		var result = await _userService.AlterarSenha(usuarioId, alterarSenhaRequest.SenhaAtual, alterarSenhaRequest.NovaSenha);

		//		if (result is null || !result.Succeeded)
		//		{
		//			foreach (var erro in result.Errors)
		//			{
		//				_notificador.AdicionarNotificacao(erro.Description);
		//			}
		//		}
		//	}

		//	catch
		//	{
		//		_notificador.AdicionarNotificacao("Ocorreu um erro ao alterar senha!");
		//	}
		//}

		//public async Task<UsuariosDropdownResponse> ObterDropdown()
		//{
		//	var usuarios = await _userService.ObterTodos().ToListAsync();
		//	return new UsuariosDropdownResponse()
		//	{
		//		Usuarios = usuarios?.Select(x => new UsuarioDropdownResponse()
		//		{
		//			Id = x.Id,
		//			Nome = x.Nome
		//		}).ToList()
		//	};
		//}

		//public async Task<AssociarPerfisResponse> AssociarPerfis(AssociarPerfisUsuarioRequest request)
		//{
		//	await _userService.AlterarPerfis(request.UsuarioId, request.PerfisId);

		//	_unitOfWork.Commit();

		//	var perfilPermissoes = await _userService.ObterPerfilPermissoes(request.UsuarioId);

		//	return new AssociarPerfisResponse()
		//	{
		//		Claims = perfilPermissoes.Select(pp => new ClaimResponse { Type = pp.ClaimType, Value = pp.ClaimValue })
		//	};
		//}

		//public Task EnviarEmailResetSenha(string email)
		//{
		//	throw new NotImplementedException();
		//}

		//public Task<ResultPaginado<UsuarioResponse>> ObterTodosPaginado(InputPaginado input)
		//{
		//	throw new NotImplementedException();
		//}

		//public Task<UsuarioResponse> Atualizar(AtualizarUsuarioRequest usuarioAtualizar)
		//{
		//	throw new NotImplementedException();
		//}

		//private Usuario MapeiaUsuarioRequestParaUsuario(UsuarioRequest registroRequest)
		//{
		//	var endereco = _mapper.Map<Endereco>(registroRequest.Endereco);

		//	return new Usuario(registroRequest.NomeUsuario, registroRequest.Nome, registroRequest.Sobrenome, registroRequest.CPFCNPJ,
		//		registroRequest.DataNascimento!.Value, registroRequest.Email, registroRequest.Telefone, endereco);
		//}

		//private async Task<Usuario> ObterUsuarioPorEmailOuUsername(LoginRequest loginRequest)
		//{
		//	var usuario = await _userManager.FindByEmailAsync(loginRequest.NomeUsuarioEmail);
		//	if (usuario is null)
		//	{
		//		usuario = await _userManager.FindByNameAsync(loginRequest.NomeUsuarioEmail);
		//	}

		//	return usuario;
		//}
	}
}
