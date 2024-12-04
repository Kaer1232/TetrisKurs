using TetrisKurs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisKurs.Model
{
    public class PageModel<T> where T : ViewModelBase, new()
    {
        public Page Page { get; set; }
        public T ViewModel { get; set; }
    }
}
