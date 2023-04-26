using Aspti.Domain.Entidades;
using Aspti.Domain.Interface.Repository;
using Aspti.Domain.Interface.Service;
using Aspti.Domain.Models;
using Aspti.Infra.CrossCutting.Notificacoes;
using Aspti.Infra.CrossCutting.Paginacao;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aspti.Domain.Service
{
	public class PerfilService : Service<Perfil>, IPerfilService
	{
		private readonly IMapper _mapper;
		private readonly IPerfilPermissaoRepository _perfilPermissaoRepository;
		private readonly IPerfilRepository _perfilRepository;
		private readonly ITelaRepository _telaRepository;
		private readonly RoleManager<Perfil> _roleManager;
		private readonly INotificador _notificador;

		public PerfilService(
			IMapper mapper,
			IPerfilRepository perfilRepository,
			IPerfilPermissaoRepository perfilPermissaoRepository,
			ITelaRepository telaRepository,
			RoleManager<Perfil> roleManager,
			INotificador notificador)
			: base(perfilRepository)
		{
			_mapper = mapper;
			_perfilRepository = perfilRepository;
			_perfilPermissaoRepository = perfilPermissaoRepository;
			_telaRepository = telaRepository;
			_roleManager = roleManager;
			_notificador = notificador;
		}

		public PerfilModel Novo()
		{
			return new PerfilModel
			{
				PerfilPermissoes = _telaRepository
										.ObterTodos()
										.Select(s => new PerfilPermissaoModel(s.Id, s.Nome))
										.ToList()
			};
		}

		public override PerfilModel ObterPorId(Guid id)
		{
			var perfil = _mapper.Map<PerfilModel>(_perfilRepository.ObterPorId(id));

			if (perfil != null)
			{
				perfil.PerfilPermissoes = _telaRepository
												.ObterTodos()
												.Select(s => new PerfilPermissaoModel(s.Id, s.Nome))
												.ToList();

				var perfilPermissoes = _perfilPermissaoRepository
											.ObterPorPerfilId(perfil.Id)
											.ToList();

				foreach (var perfilPermissao in perfilPermissoes)
				{
					var perfilPermissaoModel = perfil
													.PerfilPermissoes
													.FirstOrDefault(p => p.IdTela.ToString() == perfilPermissao.ClaimType);

					perfilPermissaoModel?.SetPermissao(perfilPermissao.ClaimValue);
				}
			}

			return perfil;
		}

		public PerfilModel Adicionar(PerfilModel obj)
		{
			var perfil = _perfilRepository
								.Adicionar(_mapper.Map<Perfil>(obj));
			obj.Id = perfil.Id;

			foreach (var perfilPermissao in obj.PerfilPermissoes)
			{
				foreach (var permisao in perfilPermissao.Permissaos)
				{
					var claim = new PerfilPermissao
					{
						RoleId = perfil.Id,
						ClaimType = perfilPermissao.IdTela.ToString(),
						ClaimValue = permisao.ToString()
					};
					_perfilPermissaoRepository.Adicionar(claim);
				}
			}

			return obj;
		}

		public void Atualizar(PerfilModel obj)
		{
			_perfilRepository.Atualizar(_mapper.Map<Perfil>(obj));

			var perfilPermissoesDB = _perfilPermissaoRepository
										.ObterPorPerfilId(obj.Id)
										.ToList();

			foreach (var perfilPermissaoDB in perfilPermissoesDB)
			{
				var exist = obj.PerfilPermissoes.Any(pp =>
					pp.IdTela.ToString() == perfilPermissaoDB.ClaimType &&
					pp.Permissaos.Any(p => p.ToString() == perfilPermissaoDB.ClaimValue));
				if (!exist)
					_perfilPermissaoRepository
						.Remover(perfilPermissaoDB);
			}

			foreach (var perfilPermissao in obj.PerfilPermissoes)
			{
				foreach (var permisao in perfilPermissao.Permissaos)
				{
					var claimType = perfilPermissao.IdTela.ToString();
					var claimValue = permisao.ToString();

					if (!perfilPermissoesDB.Any(p => p.ClaimValue == claimValue && p.ClaimType == claimType))
					{
						var claim = new PerfilPermissao
						{
							RoleId = obj.Id,
							ClaimType = claimType,
							ClaimValue = claimValue
						};
						_perfilPermissaoRepository
							.Adicionar(claim);
					}
				}
			}
		}

		public override void Remover(Guid id)
		{
			var perfilPermissoesDB = _perfilPermissaoRepository
										.ObterPorPerfilId(id)
										.ToList();

			foreach (var perfilPermissaoDB in perfilPermissoesDB)
			{
				_perfilPermissaoRepository.Remover(perfilPermissaoDB);
			}

			_perfilRepository.Remover(id);
		}

		public Task<ResultPaginado<Perfil>> ObterTodosComPermissoes(InputPaginado input)
		{
			return _perfilRepository.ObterTodosComPermissoes(input);
		}

		public Perfil AtualizarPerfil(Guid id, Perfil perfilAtualizar)
		{
			var perfil = ObterPorId(id);

			AtualizaValoresPerfil(perfil, perfilAtualizar);
			Atualizar(perfil);

			return perfil;
		}

		public async Task Delete(Guid id)
		{
			var perfil = await _perfilRepository.ObterPorIdAsync(id);
			if (perfil is null)
			{
				_notificador.AdicionarNotificacao("Perfil não existe.");
				return;
			}

			_perfilRepository.Remover(perfil);
		}

		private void AtualizaValoresPerfil(PerfilModel perfil, Perfil perfilAtualizar)
		{
			perfil.Name = perfilAtualizar.Name;
			perfil.NormalizedName = perfilAtualizar.Name.ToUpper();
		}
	}

}
