using Microsoft.EntityFrameworkCore;
using TetrisKurs.Model;

namespace TetrisKurs.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<RecordsModel> Records { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "records.db");
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Заполнение тестовыми данными при создании базы данных
            modelBuilder.Entity<RecordsModel>().HasData(
                new RecordsModel { Id = 1, Score = 1000 },
                new RecordsModel { Id = 2, Score = 950 },
                new RecordsModel { Id = 3, Score = 880 },
                new RecordsModel { Id = 4, Score = 800 },
                new RecordsModel { Id = 5, Score = 750 }
            );
        }
    }
}
