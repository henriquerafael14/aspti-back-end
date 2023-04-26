using Aspti.Domain.Entidades;
using Aspti.Domain.Interface.Repository;
using Aspti.Domain.Interface.Service;
using Aspti.Infra.CrossCutting.Autenticacao;
using Aspti.Infra.CrossCutting.Configuration;
using Aspti.Infra.CrossCutting.Notificacoes;
using Aspti.Infra.CrossCutting.Utils;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Aspti.Domain.Service
{
	public class UsuarioService : Service<Usuario>, IUsuarioService
	{
		private const string Blacklist = "blacklist";

		private readonly IUsuarioRepository _userRepository;
		//private readonly UserManager<Usuario> _userManager;
		//private readonly RoleManager<Perfil> _roleManager;
		//private readonly INotificador _notificador;
		//private readonly IDistributedCache _distributedCache;
		//private readonly IHttpContextAccessor _httpContextAccessor;

		public UsuarioService(IUsuarioRepository userRepository
			//UserManager<Usuario> userManager,
			//RoleManager<Perfil> roleManager,
			//INotificador notificador,
			//IDistributedCache distributedCache,
			//IHttpContextAccessor httpContextAccessor
			) : base(userRepository)
		{
			_userRepository = userRepository;
			//_userManager = userManager;
			//_roleManager = roleManager;
			//_notificador = notificador;
			//_distributedCache = distributedCache;
			//_httpContextAccessor = httpContextAccessor;
		}

		//public override IQueryable<Usuario> ObterTodos()
		//{
		//	return base.ObterTodos().Include(x => x.Endereco);
		//}

		//public override Usuario Adicionar(Usuario obj)
		//{
		//	obj.UserName = string.Format("{0}.{1}", obj.Nome, obj.Sobrenome).ToLower();
		//	obj.NormalizedUserName = obj.UserName.ToUpper();
		//	obj.NormalizedEmail = obj.Email.ToUpper();
		//	obj.PasswordHash = UtilHash.HashPassword("Lyncas123");
		//	obj.SecurityStamp = Guid.NewGuid().ToString("D");
		//	obj.AlterarCPFCNPJ(UtilString.SemFormatacao(obj.CPFCNPJ));
		//	obj.PhoneNumber = UtilString.SemFormatacao(obj.PhoneNumber);
		//	obj.Ativar();
		//	return base.Adicionar(obj);
		//}

		//public async Task<Usuario> ObterPorEmail(string usuarioEmail)
		//{
		//	return await _userRepository
		//				.ObterPorEmail(usuarioEmail);
		//}

		//public void Inativar(Guid id)
		//{
		//	var usuario = _userRepository
		//						.ObterPorId(id);

		//	usuario
		//		.Inativar();
		//}

		//public async Task<string> GerarTokenClaims(string email)
		//{
		//	var usuario = await _userManager.FindByEmailAsync(email);

		//	var claims = await ObterClaimsDoUsuario(usuario);

		//	var token = AutenticacaoJWT.GerarToken(claims);

		//	return token;
		//}

		//private async Task<List<Claim>> ObterClaimsDoUsuario(Usuario usuario)
		//{
		//	var claims = (await _userManager.GetClaimsAsync(usuario)).ToList();

		//	claims.AddRange(new List<Claim>
		//	{
		//		new (ClaimTypes.Name, usuario.Nome),
		//		new (ClaimTypes.NameIdentifier, usuario.Id.ToString())
		//	});

		//	var userRoles = await _userManager.GetRolesAsync(usuario);

		//	foreach (var roleName in userRoles)
		//	{
		//		claims.Add(new Claim("role", roleName));
		//		var role = await _roleManager.FindByNameAsync(roleName);
		//		var claimsInRole = await _roleManager.GetClaimsAsync(role);

		//		claims.AddRange(claimsInRole);
		//	}

		//	return claims;
		//}

		//public async Task RegistrarUsuario(Usuario usuario, string senha)
		//{
		//	var result = await _userManager.CreateAsync(usuario, senha);
		//	if (!result.Succeeded)
		//	{
		//		var erros = result.Errors.Select(x => x.Description);
		//		_notificador.AdicionarNotificacoes(erros);
		//	}
		//}

		//public bool EhTokenValido()
		//{
		//	var tokenRequisicao = ObtenhaTokenRequisicao();
		//	if (string.IsNullOrEmpty(tokenRequisicao))
		//	{
		//		_notificador.AdicionarNotificacao("É necessário um token autenticação no header da requisição.");
		//		return false;
		//	}

		//	var tokenJWT = AutenticacaoJWT.DescriptografarToken(tokenRequisicao);
		//	var tokenExpirado = DateTime.UtcNow > tokenJWT.ValidTo;

		//	if (tokenExpirado)
		//	{
		//		return false;
		//	}

		//	var blacklistPossuiToken = BlacklistPossuiToken(tokenRequisicao);
		//	return !blacklistPossuiToken;
		//}

		//public void InvalidarToken()
		//{
		//	var tokenRequisicao = ObtenhaTokenRequisicao();
		//	if (string.IsNullOrEmpty(tokenRequisicao))
		//	{
		//		_notificador.AdicionarNotificacao("É necessário um token autenticação no header da requisição.");
		//		return;
		//	}

		//	var tokenJWT = AutenticacaoJWT.DescriptografarToken(tokenRequisicao);
		//	var tokenExpirado = DateTime.UtcNow > tokenJWT.ValidTo;

		//	var blacklistPossuiToken = BlacklistPossuiToken(tokenRequisicao);
		//	if (tokenExpirado || blacklistPossuiToken)
		//	{
		//		_notificador.AdicionarNotificacao("Token já é inválido.");
		//		return;
		//	}

		//	AdicionaTokenEmBlacklist(tokenRequisicao, tokenJWT);
		//}

		//public async Task<IdentityResult> ResetSenha(Guid usuarioId, string token, string novaSenha)
		//{
		//	var user = await _userManager.FindByIdAsync(usuarioId.ToString());
		//	var result = await _userManager.ResetPasswordAsync(user, token, novaSenha);
		//	return result;
		//}

		//public async Task<IdentityResult> AlterarSenha(string usuarioId, string senhaAtual, string novaSenha)
		//{
		//	var usuario = await _userManager.FindByIdAsync(usuarioId);
		//	var result = await _userManager.ChangePasswordAsync(usuario, senhaAtual, novaSenha);

		//	return result;
		//}

		//public async Task AlterarPerfis(Guid usuarioId, List<Guid> perfisId)
		//{
		//	var usuario = await _userRepository.ObterUsuarioComPerfis(usuarioId);
		//	usuario.AtualizarPerfis(perfisId);
		//}

		//public async Task<List<PerfilPermissao>> ObterPerfilPermissoes(Guid usuarioId)
		//{
		//	var perfilPermissoes = await _userRepository.ObterPerfilPermissoes(usuarioId);
		//	return perfilPermissoes;
		//}

		//public async Task<bool> EmailJaCadastrado(string email)
		//{
		//	return await _userRepository.EmailJaCadastrado(email);
		//}

		//private void AdicionaTokenEmBlacklist(string token, JwtSecurityToken tokenJWT)
		//{
		//	var distributedCacheOptions = new DistributedCacheEntryOptions()
		//	{
		//		AbsoluteExpiration = tokenJWT.ValidTo
		//	};

		//	var key = $"{Blacklist}{token}";
		//	_distributedCache.SetString(key, "true", distributedCacheOptions);
		//}

		//private bool BlacklistPossuiToken(string token)
		//{
		//	var key = $"{Blacklist}{token}";
		//	var blacklistPossuiToken = _distributedCache.GetString(key) is not null;
		//	return blacklistPossuiToken;
		//}

		//private string ObtenhaTokenRequisicao()
		//{
		//	var headerToken = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString();
		//	var bearerToken = headerToken.Replace("Bearer", string.Empty).Trim();
		//	return bearerToken;
		//}
	}
}