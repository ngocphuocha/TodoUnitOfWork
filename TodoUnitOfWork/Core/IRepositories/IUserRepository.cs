using TodoUnitOfWork.Models;
using System;
using System.Threading.Tasks;

namespace TodoUnitOfWork.Core.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}
