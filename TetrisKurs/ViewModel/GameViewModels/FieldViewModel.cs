using TetrisKurs.Base;
using TetrisKurs.Model.GameModels;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisKurs.ViewModel.GameViewModels
{
    public class FieldViewModel
    {
        private Field Field { get; }

        public CellViewModel[,] Cells { get; }

        public IReadOnlyReactiveProperty<bool> IsActivated => Field.IsActivated;

        private Color BackgroundColor => Colors.WhiteSmoke;

        public FieldViewModel(Field field)
        {
            Field = field;

            Cells = new CellViewModel[Field.RowCount, Field.ColumnCount];
            foreach (var item in Cells.WithIndex())
                Cells[item.X, item.Y] = new CellViewModel();

            Field.Tetrimino
                .CombineLatest
                (
                    Field.PlacedBlocks,
                    (t, p) => (t == null ? p : p.Concat(t.Blocks))
                            .ToDictionary2(x => x.Position.Row, x => x.Position.Column)
                )
                .Subscribe(x =>
                {
                    foreach (var item in Cells.WithIndex())
                    {
                        var color = x.GetValueOrDefault(item.X)
                                    ?.GetValueOrDefault(item.Y)
                                    ?.Color
                                    ?? BackgroundColor;
                        item.Element.Color.Value = color;
                    }
                });
        }

        public void MoveTetrimino(MoveDirection direction) => Field.MoveTetrimino(direction);

        public void RotationTetrimino(RotationDirection direction) => Field.RotationTetrimino(direction);

        public void ForceFixTetrimino() => Field.ForceFixTetrimino();
    }
}
