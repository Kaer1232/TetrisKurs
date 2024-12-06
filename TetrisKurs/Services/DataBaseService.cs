using TetrisKurs.Data;
using TetrisKurs.Infrastructure;
using TetrisKurs.Interfaces;
using TetrisKurs.Model;

namespace TetrisKurs.Services
{
    public class DataBaseService(ApplicationContext context) : IDataBaseService
    {
        private readonly AppDbContext _dbContext;
        private int _newScore;
        public bool Add(RecordsModel records)
        {
            try
            {
                context.Records.Add(records);
                context.SaveChanges();
                return true;
            }
            catch 
            { 
                return false; 
            }
        }
    }
}
