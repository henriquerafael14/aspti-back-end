using System;

namespace Aspti.Infra.CrossCutting.Paginacao
{
	public class DadosDePaginacao
    {
        public int TotalDeElementos { get; set; }
        public int TamanhoDaPagina { get; set; }
        public int NumeroDaPagina { get; set; }
        public int TotalPages => TamanhoDaPagina == 0
            ? 0
            : (int)Math.Ceiling(TotalDeElementos / (double)TamanhoDaPagina);

        public int PrimeiraPagina => 0;

        public int UltimaPagina => TotalPages == 0 ? 0 : TotalPages - 1;

        public bool PossuiPaginaAnterior => NumeroDaPagina >= 1;

        public bool PossuiPaginaSeguinte => NumeroDaPagina < UltimaPagina;

        public int PaginaAnterior => !PossuiPaginaAnterior ? PrimeiraPagina : NumeroDaPagina - 1;

        public int PaginaSeguinte => !PossuiPaginaSeguinte ? UltimaPagina : NumeroDaPagina + 1;
    }
}
