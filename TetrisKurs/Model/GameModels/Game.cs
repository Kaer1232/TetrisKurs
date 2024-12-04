using System.Reactive.Linq;
using TetrisKurs.Model.GameModels;
using Reactive.Bindings;
using TetrisKurs.ViewModel.GameViewModels;


namespace TetrisKurs.Base
{
    public class Game
    {
        public GameResult Result { get; } = new GameResult();

        public Field Field { get; } = new Field();

        public IReadOnlyReactiveProperty<bool> IsPlaying => this.Field.IsActivated.ToReadOnlyReactiveProperty();

        public IReadOnlyReactiveProperty<bool> IsOver => this.Field.IsUpperLimitOvered.ToReadOnlyReactiveProperty();

        public IReadOnlyReactiveProperty<TetriminoKind> NextTetrimino => this.nextTetrimino;
        private readonly ReactiveProperty<TetriminoKind> nextTetrimino = new ReactiveProperty<TetriminoKind>();
        private int PreviousCount { get; set; }

        public Game()
        {
            this.Field.PlacedBlocks.Subscribe(_ =>
            {
                //--- 10 行消すたびにスピードアップ
                var count = this.Result.TotalRowCount.Value / 10;
                if (count > this.PreviousCount)
                {
                    this.PreviousCount = count;
                    this.Field.SpeedUp();
                }

                //--- 新しいテトリミノを設定
                var kind = this.nextTetrimino.Value;
                this.nextTetrimino.Value = Tetrimino.RandomKind();
                this.Field.Tetrimino.Value = Tetrimino.Create(kind);
            });
            this.Field.LastRemovedRowCount.Subscribe(this.Result.AddRowCount);
        }

        public void Play()
        {
            if (this.IsPlaying.Value)
                return;

            this.PreviousCount = 0;
            this.nextTetrimino.Value = Tetrimino.RandomKind();
            this.Field.Activate(Tetrimino.RandomKind());
            this.Result.Clear();
        }
    }
}