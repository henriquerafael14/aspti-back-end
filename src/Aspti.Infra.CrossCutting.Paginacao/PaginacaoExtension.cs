using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aspti.Infra.CrossCutting.Paginacao
{
	public static class PaginacaoExtension
    {
        public static async Task<ResultPaginado<TResultItem>> PaginarAsync<TResultItem>(
            this IQueryable<TResultItem> query,
            InputPaginado input,
            CancellationToken cancellationToken = new())
        {
            var numeroDaPagina = input.NumeroDaPagina < 0 ? 0 : input.NumeroDaPagina;
            var tamanhoDaPagina = input.TamanhoDaPagina < 1 ? 10 : input.TamanhoDaPagina;

            var totalDeElementos = await query.CountAsync(cancellationToken);

            var indexInicial = numeroDaPagina * tamanhoDaPagina;

            var itens = await query
                .Skip(indexInicial)
                .Take(tamanhoDaPagina)
                .ToListAsync(cancellationToken);

            var result = new ResultPaginado<TResultItem>
            {
                Itens = itens,
                Paginacao = new DadosDePaginacao
                {
                    NumeroDaPagina = numeroDaPagina,
                    TamanhoDaPagina = itens.Count,
                    TotalDeElementos = totalDeElementos
                }
            };

            return result;
        }
    }
}
