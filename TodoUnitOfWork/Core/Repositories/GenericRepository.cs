using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TodoUnitOfWork.Core.IRepositories;
using TodoUnitOfWork.Data;

namespace TodoUnitOfWork.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApiDbContext _context;
        protected DbSet<T> dbSet;
        protected readonly ILogger _logger;
        public GenericRepository(ApiDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            this.dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> All()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }


        public virtual Task<bool> Upsert(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }
    }
}
