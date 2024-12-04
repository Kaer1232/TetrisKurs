using TetrisKurs.Model;
using Microsoft.EntityFrameworkCore;

namespace TetrisKurs.Infrastructure
{
    public sealed class ApplicationContext: DbContext
    {
        public DbSet<RecordsModel> Records => Set<RecordsModel>();

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) => Database.EnsureCreated();
    }
}
