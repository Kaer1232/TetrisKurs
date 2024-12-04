using TetrisKurs.Base;

namespace TetrisKurs.Model
{
    public class ContentViewModel<T> where T : ViewModelBase, new()
    {
        public ContentView contentView { get; set; }
        public T ViewModel { get; set; }
    }
}
