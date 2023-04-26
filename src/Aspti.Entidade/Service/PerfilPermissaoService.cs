using Aspti.Domain.Entidades;
using Aspti.Domain.Interface.Repository;
using Aspti.Domain.Interface.Service;
using Aspti.Infra.CrossCutting.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aspti.Domain.Service
{
	public class PerfilPermissaoService : Service<PerfilPermissao>, IPerfilPermissaoService
	{
		private readonly IPerfilPermissaoRepository _roleClaimRepository;

		public PerfilPermissaoService(IPerfilPermissaoRepository roleClaimRepository)
			: base(roleClaimRepository)
		{
			_roleClaimRepository = roleClaimRepository;
		}

		public bool PossuiPermissaoUsuarioLogado(
			List<Guid> perfilUsuarioLogadoIds,
			PermissoesEnum permissao,
			string telaId)
		{
			if (perfilUsuarioLogadoIds == null)
				throw new ArgumentNullException(nameof(perfilUsuarioLogadoIds));

			if (string.IsNullOrEmpty(telaId))
				throw new ArgumentNullException(nameof(telaId));

			return _roleClaimRepository
						.PossuiPermissaoUsuarioLogado(perfilUsuarioLogadoIds, permissao.ToString(), telaId);

		}

		public async Task AtualizarPermissoes(Guid perfilId, List<PerfilPermissao> permissoes)
		{
			var perfilPermissoes = await _roleClaimRepository.ObterPorPerfilId(perfilId)
								   .Select(p => p.Id)
								   .ToListAsync();

			foreach (var perfilPermissao in perfilPermissoes)
			{
				_roleClaimRepository.Remover(perfilPermissao);
			}

			_roleClaimRepository.AdicionarVarios(permissoes);
		}
	}
}
