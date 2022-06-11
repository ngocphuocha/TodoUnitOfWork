using TodoUnitOfWork.Models;
using TodoUnitOfWork.Core.IRepositories;
using TodoUnitOfWork.Data;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TodoUnitOfWork.Core.Repositories
{
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApiDbContext context, ILogger logger) : base(context, logger) { }
        
        public override async Task<IEnumerable<User>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(UserRepository));
                return new List<User>();
            }
        }
        public virtual async Task<bool> Add(User entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public override async Task<bool> Upsert(User entity)
        {
            try
            {
                var existingUser = await dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingUser == null)
                    return await Add(entity);

                existingUser.FirstName = entity.FirstName;
                existingUser.LastName = entity.LastName;
                existingUser.Email = entity.Email;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(UserRepository));
                return false;
            }
        }
        
        public async Task<bool> Delete(int id)
        {
          try
            {
                var exixtingUser = await dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
                
                if(exixtingUser == null )
                {
                    return false;
                }
             
                dbSet.Remove(exixtingUser);

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(UserRepository));
                return false;
            }
        }

    }
}
