using System.Threading.Tasks;
using TodoUnitOfWork.Core.IRepositories;

namespace TodoUnitOfWork.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }

        Task CompleteAsync();
    }
}
