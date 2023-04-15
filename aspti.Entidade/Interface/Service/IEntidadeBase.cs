using System;

namespace Aspti.Domain.Interface.Service
{
	public interface IEntidadeBase
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
