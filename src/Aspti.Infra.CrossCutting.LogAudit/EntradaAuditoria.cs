using Aspti.Infra.CrossCutting.Enums;
using Aspti.Infra.CrossCutting.LogAudit.Model;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Aspti.Infra.CrossCutting.LogAudit
{
	public class EntradaAuditoria
	{
		public Guid Id { get; set; }
		public EntityEntry Entidade { get; }
		public EntityState TipoAuditoria { get; set; }
		public string IdEntidade { get; set; }
		public Guid? UsuarioId { get; set; }
		public string Usuario { get; set; }
		public string NomeEntidade { get; set; }
		public string NomeTabela { get; set; }
		public List<AuditoriaPropriedade> Propriedades { get; set; }

		public EntradaAuditoria(EntityEntry entidade, Guid? usuarioId, string usuario)
		{
			Entidade = entidade;
			Id = Guid.NewGuid();
			Propriedades = new List<AuditoriaPropriedade>();
			UsuarioId = usuarioId;
			Usuario = usuario;
			SetChanges();
		}

		private void SetChanges()
		{
			NomeEntidade = Entidade.Metadata.Name;
			NomeTabela = Entidade.Metadata.GetTableName();
			TipoAuditoria = Entidade.State;
			var oldValues = Entidade.GetDatabaseValues();
			foreach (var property in Entidade.Properties)
			{
				if (property.Metadata.IsPrimaryKey())
					IdEntidade = property.CurrentValue?.ToString();
				if (TipoAuditoria == EntityState.Modified && oldValues?.GetValue<object>(property.Metadata.GetColumnName())?.ToString() == property.CurrentValue?.ToString())
					continue;
				string originalValue = TipoAuditoria != EntityState.Added ? oldValues?.GetValue<object>(property.Metadata.GetColumnName())?.ToString() : null;
				string currentlValue = TipoAuditoria != EntityState.Deleted ? property.CurrentValue?.ToString() : null;
				Propriedades.Add(new AuditoriaPropriedade(Id, property.Metadata.Name, originalValue, currentlValue, property.Metadata.IsPrimaryKey()));
			}
		}

		public Auditoria ToAudit()
		{
			var auditoria = new Auditoria
			{
				Id = Id,
				DataAuditoria = DateTime.Now,
				TipoAuditoria = (TipoAuditoriaEnum)TipoAuditoria,
				IdEntidade = IdEntidade,
				NomeEntidade = NomeEntidade,
				NomeTabela = NomeTabela,
				UsuarioId = UsuarioId,
				Usuario = Usuario,
				Propriedades = Propriedades
			};

			return auditoria;
		}
	}

}
