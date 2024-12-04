using Reactive.Bindings;

namespace TetrisKurs.ViewModel.GameViewModels
{
    public class CellViewModel
    {
        public ReactiveProperty<Color> Color { get; } = new ReactiveProperty<Color>();
    }
}
