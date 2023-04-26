using Aspti.Infra.CrossCutting.Enums;
using System;

namespace Aspti.Domain.Entidades
{
	public class Tela : EntidadeBase
	{
		public Tela()
		{
			Id = Guid.NewGuid();
		}

		public string Nome { get; private set; }
		public TelaStatusEnum Status { get; private set; }
		public string Icone { get; private set; }
		public int Ordem { get; private set; }
		public Guid CategoriaTelaId { get; protected set; }
		public virtual CategoriaTela CategoriaTela { get; protected set; }

		public void AlterarNome(string nome)
		{
			Nome = nome;
		}

		public void AlterarStatus(TelaStatusEnum status)
		{
			Status = status;
		}

		public void AlterarIcone(string icone)
		{
			Icone = icone;
		}

		public void AlterarOrdem(int ordem)
		{
			Ordem = ordem;
		}

		public void AlterarCategoriaTela(CategoriaTela categoriaTela)
		{
			CategoriaTela = categoriaTela;
		}
	}

}
