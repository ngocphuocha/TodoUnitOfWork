using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TodoUnitOfWork.Core.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> All();
        Task<T> GetById(int id);
        Task<bool> Add(T entity);
        Task<bool> Upsert(T entity);
        Task<bool> Delete(int id);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    }
}
