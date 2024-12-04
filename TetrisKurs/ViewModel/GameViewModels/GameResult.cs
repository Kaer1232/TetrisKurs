using Reactive.Bindings;
using System.Reactive.Linq;

namespace TetrisKurs.ViewModel.GameViewModels
{
    public class GameResult
    {
        public IReadOnlyReactiveProperty<int> TotalRowCount { get; }
        public IReadOnlyReactiveProperty<int> RowCount1 => rowCount1;

        private readonly ReactiveProperty<int> rowCount1 = new ReactiveProperty<int>();

        public IReadOnlyReactiveProperty<int> RowCount2 => rowCount2;

        private readonly ReactiveProperty<int> rowCount2 = new ReactiveProperty<int>();

        public IReadOnlyReactiveProperty<int> RowCount3 => rowCount3;
        private readonly ReactiveProperty<int> rowCount3 = new ReactiveProperty<int>();

        public IReadOnlyReactiveProperty<int> RowCount4 => rowCount4;
        private readonly ReactiveProperty<int> rowCount4 = new ReactiveProperty<int>();

        public GameResult()
        {
            TotalRowCount
                = RowCount1.CombineLatest
                (
                    RowCount2,
                    RowCount3,
                    RowCount4,
                    (x1, x2, x3, x4) => x1 * 40
                                    + x2 * 100
                                    + x3 * 300
                                    + x4 * 1200
                )
                .ToReadOnlyReactiveProperty();
        }
        public void AddRowCount(int count)
        {
            switch (count)
            {
                case 1: rowCount1.Value++; break;
                case 2: rowCount2.Value++; break;
                case 3: rowCount3.Value++; break;
                case 4: rowCount4.Value++; break;
                default: throw new ArgumentOutOfRangeException(nameof(count));
            }
        }
        public void Clear()
        {
            rowCount1.Value = 0;
            rowCount2.Value = 0;
            rowCount3.Value = 0;
            rowCount4.Value = 0;
        }
    }
}
