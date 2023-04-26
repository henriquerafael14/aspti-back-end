using System;
using System.Threading.Tasks;

namespace Aspti.Infra.CrossCutting.UoW
{
	public interface IUnitOfWork : IDisposable
    {
        void Commit();
        Task CommitAsync();
    }
}
