using System.Collections.Generic;

namespace Aspti.Infra.CrossCutting.Paginacao
{
	public class ResultPaginado<TResult>
    {
        public IEnumerable<TResult> Itens { get; set; }
        public DadosDePaginacao Paginacao { get; set; }
    }

}
