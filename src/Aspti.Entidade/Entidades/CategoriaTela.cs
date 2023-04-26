using System.Collections.Generic;

namespace Aspti.Domain.Entidades
{
	public class CategoriaTela : EntidadeBase
	{
		public CategoriaTela()
		{
			Telas = new List<Tela>();
		}

		public string Nome { get; private set; }
		public int Ordem { get; private set; }
		public virtual ICollection<Tela> Telas { get; private set; }

		public void AlterarNome(string nome)
		{
			Nome = nome;
		}

		public void AlterarOrdem(int ordem)
		{
			Ordem = ordem;
		}

		public void AlterarTelas(ICollection<Tela> telas)
		{
			Telas = telas;
		}
	}

}