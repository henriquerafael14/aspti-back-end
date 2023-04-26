using Aspti.Domain.Interface.Service;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aspti.Domain.Entidades
{
	public class EntidadeBase : IEntidadeBase
	{
        public EntidadeBase()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Column(Order = 98)]
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        [Column(Order = 99)]
        public DateTime DataAtualizacao { get; set; } = DateTime.Now;
    }
}
