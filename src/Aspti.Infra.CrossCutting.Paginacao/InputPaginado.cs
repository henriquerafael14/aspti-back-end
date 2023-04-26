namespace Aspti.Infra.CrossCutting.Paginacao
{
	public class InputPaginado
    {
        public int TamanhoDaPagina { get; set; }

        /// <summary>
        /// O número da pagina, iniciando em 0
        /// </summary>
        public int NumeroDaPagina { get; set; }
    }

}
