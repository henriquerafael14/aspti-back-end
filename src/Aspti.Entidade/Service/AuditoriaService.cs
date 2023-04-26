using Aspti.Domain.Interface.Repository;
using Aspti.Domain.Interface.Service;
using Aspti.Infra.CrossCutting.LogAudit.Model;
using Aspti.Infra.CrossCutting.Paginacao;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aspti.Domain.Service
{
	public class AuditoriaService : Service<Auditoria>, IAuditoriaService
	{
		private readonly IAuditoriaRepository _auditoriaRepository;

		public AuditoriaService(IAuditoriaRepository auditoriaRepository)
			: base(auditoriaRepository)
		{
			_auditoriaRepository = auditoriaRepository;
		}

		//public async Task<ResultPaginado<Auditoria>> Obter(FiltroAuditoriaDTO filtro)
		//{
		//	var filterExpression = ObterFilterExpression(filtro);

		//	var auditoriaQuery = _auditoriaRepository.ObterTodos()
		//		.Include(x => x.Propriedades)
		//		.Where(filterExpression)
		//		.OrderBy(x => x.DataAuditoria);

		//	var resultPaginado = await auditoriaQuery.PaginarAsync(filtro);

		//	return resultPaginado;
		//}

		public async Task<List<string>> ObterEntidades()
		{
			var entidadesAuditoria = await _auditoriaRepository.ObterTodos()
				.Select(auditoria => auditoria.NomeTabela).Distinct().ToListAsync();

			return entidadesAuditoria;
		}

		public async Task<List<string>> ObterCampos(string entidade)
		{
			var auditoriaPropriedades = await _auditoriaRepository.ObterTodos()
				.Include(x => x.Propriedades)
				.Where(x => x.NomeTabela == entidade)
				.SelectMany(x => x.Propriedades)
				.ToListAsync();

			var campos = auditoriaPropriedades.Select(x => x.NomeDaPropriedade).Distinct().ToList();
			return campos;
		}

		//private Expression<Func<Auditoria, bool>> ObterFilterExpression(FiltroAuditoriaDTO filtro)
		//{
		//	var expression = PredicateBuilder.New<Auditoria>(true);

		//	if (filtro.DataDe.HasValue) expression = expression.And(x => x.DataAuditoria.Date >= filtro.DataDe);

		//	if (filtro.DataAte.HasValue) expression = expression.And(x => x.DataAuditoria.Date <= filtro.DataAte);

		//	if (filtro.UsuarioId.HasValue) expression = expression.And(x => x.UsuarioId == filtro.UsuarioId);

		//	if (filtro.TipoAuditoria.HasValue) expression = expression.And(x => x.TipoAuditoria == filtro.TipoAuditoria);

		//	if (!string.IsNullOrEmpty(filtro.NomeTabela)) expression = expression.And(x => Equals(x.NomeTabela.ToUpper(), filtro.NomeTabela.ToUpper()));

		//	if (!string.IsNullOrEmpty(filtro.Valor)) expression = expression.And(x => x.Propriedades.Any(p => Equals(filtro.Valor.ToUpper(), p.ValorNovo.ToUpper())));

		//	if (!string.IsNullOrEmpty(filtro.NomeDaPropriedade)) expression = expression.And(x => x.Propriedades.Any(p => Equals(filtro.NomeDaPropriedade, p.NomeDaPropriedade)));

		//	return expression;
		//}
	}


}
