using System.Collections.Generic;

namespace Aspti.Infra.CrossCutting.Constantes
{
	public struct ConstantesSistema
	{
		public struct Telas
		{
			public const string Inicio = "Inicio";
			public const string Empresa = "Empresa";
			public const string Usuario = "Usuario";
			public const string UsuarioPerfil = "UsuarioPerfil";
			public const string PerfilPermissao = "PerfilPermissao";
			public const string Auditoria = "Auditoria";
			public const string Perfil = "Perfil";
			public const string UsuarioPermissao = "UsuarioPermissao";
			public const string CategoriaTela = "CategoriaTela";
			public const string Tela = "Tela";
		}

		public struct Categoria
		{
			public const string Inicio = "Inicio";
			public const string Administracao = "Administracao";
			public const string Configuracao = "Configuracao";
			public const string Seguranca = "Seguranca";
		}

		public struct ControllerPermissao
		{
			public const string Usuario = "Usuario";
			public const string Perfil = "Perfil";

			public static List<string> Todos = new()
		{
			Usuario,
			Perfil
		};
		}

		public struct DescricaoAutenticacao
		{
			public const string Autenticar = "Realiza autenticação e retorna token jwt";
			public const string InvalidarToken = "Realiza autenticação e retorna token jwt";
			public const string TokenValido = "Retorna validade do token";
		}

		public struct DescricaoUsuario
		{
			public const string Registrar = "Registro de usuário";
			public const string ObterTodos = "Obtém todos usuários";
			public const string ObterPorId = "Obtém usuário por id";
			public const string Atualizar = "Atualiza dados de usuário";
			public const string Remover = "Remove usuário";
			public const string SolicitarResetDeSenha = "Envia token para reset de senha para e-mail especificado";
			public const string ResetDeSenha = "Realiza reset de senha a partir de token recebido por e-mail";
			public const string AlterarSenha = "Altera senha de usuário";
			public const string ObterDropdown = "Obtém ids para ser utilizado como filtro";
			public const string AssociarPerfis = "Associa perfis ao usuário";
		}

		public struct DescricaoPerfil
		{
			public const string Criar = "Cria perfil";
			public const string ObterTodos = "Obtém todos perfis";
			public const string ObterPorId = "Obtém perfil por id";
			public const string Atualizar = "Atualiza dados de perfil";
			public const string Remover = "Remove perfil";
		}

		public struct DescricaoAuditoria
		{
			public const string Obter = "Obtém auditorias de acordo com filtros";
			public const string ObterEntidades = "Obtém entidades para ser utilizada como filtro";
			public const string ObterCampos = "Obtém campos para ser utilizado como filtro";
		}

		public struct DescricaoCep
		{
			public const string Cep = "Obtém endereço através do CEP";
		}
	}
}
