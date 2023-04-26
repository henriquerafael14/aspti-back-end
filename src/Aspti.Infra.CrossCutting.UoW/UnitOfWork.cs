using Aspti.Infra.Data.Context;
using System.Threading.Tasks;

namespace Aspti.Infra.CrossCutting.UoW
{
	public class UnitOfWork : IUnitOfWork
    {
        private readonly AsptiDbContext _context;

        public UnitOfWork(AsptiDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (_context != null) _context.Dispose();
        }
    }
}
