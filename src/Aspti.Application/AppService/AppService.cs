using Aspti.Application.AppService.Interface;
using Aspti.Domain.Interface.Service;
using Aspti.Infra.CrossCutting.Notificacoes;
using Aspti.Infra.CrossCutting.UoW;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aspti.Application.AppService
{
	public class AppService<TEntity> : IAppService<TEntity> where TEntity : class, IEntidadeBase
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _uow;
		private readonly INotificador _notificador;
		private readonly IService<TEntity> _service;

		public AppService(
			IMapper mapper,
			IUnitOfWork uow,
			INotificador notificador,
			IService<TEntity> service)
		{
			_notificador = notificador;
			_mapper = mapper;
			_uow = uow;
			_service = service;
		}

		public virtual async Task<TEntityResult> ObterPorIdAsync<TEntityResult>(Guid id)
		{
			var entidade = await _service.ObterPorIdAsync(id);

			return _mapper.Map<TEntityResult>(entidade);
		}

		public virtual async Task<IEnumerable<TEntityResult>> ObterTodosAsync<TEntityResult>()
		{
			var entidades = await _service.ObterTodosAsync();

			return _mapper.Map<IEnumerable<TEntityResult>>(entidades);
		}

		public virtual async Task RemoverAsync(Guid id)
		{
			var entidade = await _service.ObterPorIdAsync(id);

			if (entidade == null)
			{
				_notificador.AdicionarNotificacao("Entidade não encontrada");
				return;
			}

			_service.Remover(entidade);

			await _uow.CommitAsync();
		}
	}
}
