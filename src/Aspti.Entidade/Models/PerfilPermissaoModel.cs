using Aspti.Infra.CrossCutting.Enums;
using System;
using System.Collections.Generic;

namespace Aspti.Domain.Models
{
	public class PerfilPermissaoModel
	{
		public PerfilPermissaoModel()
		{

		}

		public PerfilPermissaoModel(Guid idTela, string nomeTela)
		{
			IdTela = idTela;
			NomeTela = nomeTela;
		}
		public Guid IdTela { get; set; }
		public string NomeTela { get; set; }
		public bool Visualizar { get; set; }
		public bool Listar { get; set; }
		public bool Detalhes { get; set; }
		public bool Criar { get; set; }
		public bool Excluir { get; set; }
		public bool Editar { get; set; }

		public List<PermissoesEnum> Permissaos
		{
			get
			{
				List<PermissoesEnum> permissoes = new List<PermissoesEnum>();
				if (Visualizar) permissoes.Add(PermissoesEnum.Visualizar);
				if (Listar) permissoes.Add(PermissoesEnum.Listar);
				if (Detalhes) permissoes.Add(PermissoesEnum.Detalhes);
				if (Criar) permissoes.Add(PermissoesEnum.Criar);
				if (Excluir) permissoes.Add(PermissoesEnum.Excluir);
				if (Editar) permissoes.Add(PermissoesEnum.Editar);

				return permissoes;
			}
		}

		public void SetPermissao(string value)
		{
			var permissao = (PermissoesEnum)Enum.Parse(typeof(PermissoesEnum), value, true);

			switch (permissao)
			{
				case PermissoesEnum.Visualizar:
					Visualizar = true;
					break;
				case PermissoesEnum.Listar:
					Listar = true;
					break;
				case PermissoesEnum.Detalhes:
					Detalhes = true;
					break;
				case PermissoesEnum.Criar:
					Criar = true;
					break;
				case PermissoesEnum.Excluir:
					Excluir = true;
					break;
				case PermissoesEnum.Editar:
					Editar = true;
					break;
			}
		}
	}

}
