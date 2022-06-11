using Microsoft.EntityFrameworkCore;
using TodoUnitOfWork.Models;

namespace TodoUnitOfWork.Data
{
    public class ApiDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
