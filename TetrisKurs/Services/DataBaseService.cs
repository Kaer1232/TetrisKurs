using TetrisKurs.Infrastructure;
using TetrisKurs.Interfaces;
using TetrisKurs.Model;

namespace TetrisKurs.Services
{
    public class DataBaseService(ApplicationContext context) : IDataBaseService
    {
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
