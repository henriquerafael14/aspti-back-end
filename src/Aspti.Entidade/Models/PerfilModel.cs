using Aspti.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspti.Domain.Models
{
	public class PerfilModel : Perfil
	{
		public List<PerfilPermissaoModel> PerfilPermissoes { get; set; }
	}

}
