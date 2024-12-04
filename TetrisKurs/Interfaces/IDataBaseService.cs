using TetrisKurs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisKurs.Interfaces
{
    public interface IDataBaseService
    {
        bool Add(RecordsModel records);
    }
}
