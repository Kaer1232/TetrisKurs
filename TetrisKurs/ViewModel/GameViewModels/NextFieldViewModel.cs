using TetrisKurs.Base;
using TetrisKurs.Model.GameModels;
using Reactive.Bindings;
using System.Reactive.Linq;

/* Необъединенное слияние из проекта "TetrisKurs (net9.0-ios)"
До:
using This = TetrisKurs.ViewModel.GameViewModels.NextFieldViewModel;
После:
using This = TetrisKurs.ViewModel.GameViewModel.NextFieldViewModel;
using TetrisKurs;
using TetrisKurs.ViewModel;
using TetrisKurs.ViewModel.GameViewModels;
using TetrisKurs.ViewModel.GameViewModel;
*/
using This = TetrisKurs.ViewModel.GameViewModels.NextFieldViewModel;

namespace TetrisKurs.ViewModel.GameViewModels
{
    public class NextFieldViewModel
    {
        private const byte RowCount = 5;

        private const byte ColumnCount = 5;

        public CellViewModel[,] Cells { get; }

        private Color BackgroundColor => Colors.WhiteSmoke;

        public NextFieldViewModel(IReadOnlyReactiveProperty<TetriminoKind> nextTetrimino)
        {
            Cells = new CellViewModel[This.RowCount, This.ColumnCount];
            foreach (var item in Cells.WithIndex())
                Cells[item.X, item.Y] = new CellViewModel();

            nextTetrimino
                .Select(x => Tetrimino.Create(x).Blocks.ToDictionary2(y => y.Position.Row, y => y.Position.Column))
                .Subscribe(x =>
                {
                    var offset = new Position((-6 - x.Count) / 2, 2);

                    foreach (var item in Cells.WithIndex())
                    {
                        var color = x.GetValueOrDefault(item.X + offset.Row)
                                    ?.GetValueOrDefault(item.Y + offset.Column)
                                    ?.Color
                                    ?? BackgroundColor;
                        item.Element.Color.Value = color;
                    }
                });
        }
    }
}
