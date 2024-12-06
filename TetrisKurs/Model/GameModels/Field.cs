using Reactive.Bindings;
using System.Reactive.Linq;
using TetrisKurs.Base;
using This = TetrisKurs.Model.GameModels.Field;

namespace TetrisKurs.Model.GameModels
{
    public class Field
    {
        public const byte RowCount = 24;
        public const byte ColumnCount = 10;
        public IReadOnlyReactiveProperty<IReadOnlyList<Block>> PlacedBlocks => this.placedBlocks;
        private readonly ReactiveProperty<IReadOnlyList<Block>> placedBlocks = new ReactiveProperty<IReadOnlyList<Block>>(Array.Empty<Block>(), ReactivePropertyMode.RaiseLatestValueOnSubscribe);

        public ReactiveProperty<Tetrimino> Tetrimino { get; } = new ReactiveProperty<Tetrimino>();


        public IReadOnlyReactiveProperty<bool> IsActivated => this.isActivated;
        private readonly ReactiveProperty<bool> isActivated = new ReactiveProperty<bool>(mode: ReactivePropertyMode.DistinctUntilChanged);



        public IReadOnlyReactiveProperty<bool> IsUpperLimitOvered => this.isUpperLimitOvered;
        private readonly ReactiveProperty<bool> isUpperLimitOvered = new ReactiveProperty<bool>(mode: ReactivePropertyMode.DistinctUntilChanged);


        public IReadOnlyReactiveProperty<int> LastRemovedRowCount => this.lastRemovedRowCount;
        private readonly ReactiveProperty<int> lastRemovedRowCount = new ReactiveProperty<int>(mode: ReactivePropertyMode.None);

        private System.Timers.Timer Timer { get; } = new System.Timers.Timer();


        public void Speed(int choice)
        {
            int interval;
            switch (choice)
            {
                case 1: interval = 500; break;
                case 2: interval = 200; break;
                case 3: interval = 100; break;
                default: interval = 1000; break;
            }
            this.Timer.Interval = interval;
        }

        public Field()
        {
            this.Timer.ElapsedAsObservable()
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(x => this.MoveTetrimino(MoveDirection.Down));
        }


        public void Activate(TetriminoKind kind, int choice)
        {
            this.isActivated.Value = true;
            this.isUpperLimitOvered.Value = false;
            this.Tetrimino.Value = GameModels.Tetrimino.Create(kind);
            this.placedBlocks.Value = Array.Empty<Block>();
            Speed(choice);
            this.Timer.Start();
            this.isActivated.Value = !this.isUpperLimitOvered.Value;
            if (!this.isActivated.Value) this.Timer.Stop();

            System.Diagnostics.Debug.WriteLine("Start Game!");
            System.Diagnostics.Debug.WriteLine($"IsUpperLimitOvered: {IsUpperLimitOvered.Value}, isActivated: {isActivated.Value}");
        }


        public void MoveTetrimino(MoveDirection direction)
        {
            if (!this.isActivated.Value)
                return;

            if (direction == MoveDirection.Down)
            {
                Block block;
                this.Timer.Stop();
                if (this.Tetrimino.Value.Move(direction, this.CheckCollision))
                    { this.Tetrimino.ForceNotify();
                }
                else this.FixTetrimino();
                this.Timer.Start();
                return;
            }

            if (this.Tetrimino.Value.Move(direction, this.CheckCollision))
                this.Tetrimino.ForceNotify();
            
        }
        private void GameOver()
        {
            isActivated.Value = false;
            isUpperLimitOvered.Value = true;
            this.Timer.Stop();
            System.Diagnostics.Debug.WriteLine("Game Over!");
            System.Diagnostics.Debug.WriteLine($"IsUpperLimitOvered: {IsUpperLimitOvered.Value}, isActivated: {isActivated.Value}");

        }


        public void RotationTetrimino(RotationDirection direction)
        {
            if (!this.isActivated.Value)
                return;

            if (this.Tetrimino.Value.Rotation(direction, this.CheckCollision))
                this.Tetrimino.ForceNotify();
        }

        public void ForceFixTetrimino()
        {
            if (!this.isActivated.Value)
                return;

            this.Timer.Stop();
            while (this.Tetrimino.Value.Move(MoveDirection.Down, this.CheckCollision)) ;
            this.FixTetrimino();
            this.Timer.Start();
        }

        private void FixTetrimino()
        {
            var result = this.RemoveAndFixBlock();
            var removedRowCount = result.Item1;
            if (removedRowCount > 0)
                this.lastRemovedRowCount.Value = removedRowCount; if (result.Item2.Any(x => x.Position.Row < 0))
            {
                GameOver();
                return;
            }
            this.Tetrimino.Value = null;
            this.placedBlocks.Value = result.Item2;
        }
        public void SpeedUp()
        {
            const int min = 15;
            var interval = this.Timer.Interval / 15;
            this.Timer.Interval = Math.Max(interval, min);
        }
        private bool CheckCollision(Block block)
        {
            if (block == null)
                throw new ArgumentNullException(nameof(block));

            if (block.Position.Column < 0)
                return true;

            if (This.ColumnCount <= block.Position.Column)
                return true;

            if (This.RowCount <= block.Position.Row)
                return true;

            return this.placedBlocks.Value.Any(x => x.Position == block.Position);
        }
        private Tuple<int, Block[]> RemoveAndFixBlock()
        {
            var rows = this.placedBlocks.Value
                        .Concat(this.Tetrimino.Value.Blocks)
                        .GroupBy(x => x.Position.Row)
                        .Select(x => new
                        {
                            Row = x.Key,
                            IsFilled = This.ColumnCount <= x.Count(),
                            Blocks = x,
                        })
                        .ToArray();

            var blocks = rows
                        .OrderByDescending(x => x.Row)
                        .WithIndex(x => x.IsFilled)
                        .Where(x => !x.Element.IsFilled)
                        .SelectMany(x =>
                        {
                            if (x.Index == 0)
                                return x.Element.Blocks;

                            return x.Element.Blocks.Select(y =>
                            {
                                var position = new Position(y.Position.Row + x.Index, y.Position.Column);
                                return new Block(y.Color, position);
                            });
                        })
                        .ToArray();

            var removedRowCount = rows.Count(x => x.IsFilled);
            return Tuple.Create(removedRowCount, blocks);
        }
    }
}