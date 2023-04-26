using Aspti.Infra.CrossCutting.LogAudit.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aspti.Infra.CrossCutting.LogAudit
{
	public class Startup
	{
		private readonly ILogAuditoriaDbContext Db;

		public Startup(ILogAuditoriaDbContext db)
		{
			Db = db;
		}

		public void AddAuditLogs(Guid? userId, string userName)
		{
			try
			{
				Db.ChangeTracker.DetectChanges();
				var auditEntries = new List<EntradaAuditoria>();
				foreach (var entry in Db.ChangeTracker.Entries())
				{
					if (entry.Entity is Auditoria || entry.State == EntityState.Detached ||
						entry.State == EntityState.Unchanged)
						continue;
					var auditEntry = new EntradaAuditoria(entry, userId, userName);
					auditEntries.Add(auditEntry);
				}

				if (auditEntries.Any())
				{
					var logs = auditEntries.Select(x => x.ToAudit());
					Db.Auditoria.AddRange(logs);
				}
			}
			catch
			{
			}
		}
	}
}
