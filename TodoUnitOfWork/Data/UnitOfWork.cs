using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TodoUnitOfWork.Core.IConfiguration;
using TodoUnitOfWork.Core.IRepositories;
using TodoUnitOfWork.Core.Repositories;

namespace TodoUnitOfWork.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApiDbContext _context;
        private readonly ILogger _logger;
        public IUserRepository Users { get; private set; }

        public UnitOfWork(
            ApiDbContext context,
            ILoggerFactory loggerFactory
        )
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            Users = new UserRepository(_context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
