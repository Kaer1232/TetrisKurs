using System.Reactive.Linq;
using TetrisKurs.Model.GameModels;
using Reactive.Bindings;
using TetrisKurs.ViewModel.GameViewModels;
using TetrisKurs.ViewModel;


namespace TetrisKurs.Base
{
    public class Game
    {
        public GameResult Result { get; } = new GameResult();

        public IReadOnlyReactiveProperty<bool> IsOver { get; }
        public IReadOnlyReactiveProperty<bool> IsPlaying { get; }

        public Field Field { get; } = new Field();

       
        public IReadOnlyReactiveProperty<TetriminoKind> NextTetrimino => this.nextTetrimino;
        private readonly ReactiveProperty<TetriminoKind> nextTetrimino = new ReactiveProperty<TetriminoKind>();
        private int PreviousCount { get; set; }

        public Game()
        {   
            IsOver = this.Field.IsUpperLimitOvered.ToReadOnlyReactiveProperty();
            IsPlaying = this.Field.IsActivated.ToReadOnlyReactiveProperty();
            this.Field.PlacedBlocks.Subscribe(_ =>
            {
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

        public void Play(int choice)
        {
            if (this.IsPlaying.Value)
                return;

            this.PreviousCount = 0;
            this.nextTetrimino.Value = Tetrimino.RandomKind();
            this.Field.Activate(Tetrimino.RandomKind(), choice);
            this.Result.Clear();
        }
    }
}