using Reactive.Bindings;

namespace TetrisKurs.ViewModel.GameViewModels
{
    public class GameResultViewModel
    {
        private GameResult Result { get; }

        public IReadOnlyReactiveProperty<int> TotalRowCount => Result.TotalRowCount;

        public IReadOnlyReactiveProperty<int> RowCount1 => Result.RowCount1;

        public IReadOnlyReactiveProperty<int> RowCount2 => Result.RowCount2;

        public IReadOnlyReactiveProperty<int> RowCount3 => Result.RowCount3;

        public IReadOnlyReactiveProperty<int> RowCount4 => Result.RowCount4;

        public GameResultViewModel(GameResult result)
        {
            Result = result;
        }
    }
}
