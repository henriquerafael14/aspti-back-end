using System;
using System.Collections.Generic;

namespace Aspti.Application.Response
{
	public class AssociarPerfisUsuarioRequest
    {
        public Guid UsuarioId { get; set; }
        public List<Guid> PerfisId { get; set; }
    }
}
