using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Timers;
using TetrisKurs.Model.GameModels;
using Reactive.Bindings;
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


        #region コンストラクタ
        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        public Field()
        {
            this.Timer.ElapsedAsObservable()
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(x => this.MoveTetrimino(MoveDirection.Down));
        }


        public void Activate(TetriminoKind kind)
        {
            this.isActivated.Value = true;
            this.isUpperLimitOvered.Value = false;
            this.Tetrimino.Value = GameModels.Tetrimino.Create(kind);
            this.placedBlocks.Value = Array.Empty<Block>();
            this.Timer.Interval = 1000;
            this.Timer.Start();
        }


        public void MoveTetrimino(MoveDirection direction)
        {
            if (!this.isActivated.Value)
                return;

            if (direction == MoveDirection.Down)
            {
                this.Timer.Stop();
                if (this.Tetrimino.Value.Move(direction, this.CheckCollision)) this.Tetrimino.ForceNotify();
                else this.FixTetrimino();
                this.Timer.Start();
                return;
            }

            if (this.Tetrimino.Value.Move(direction, this.CheckCollision))
                this.Tetrimino.ForceNotify();
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
                this.lastRemovedRowCount.Value = removedRowCount;

            if (result.Item2.Any(x => x.Position.Row < 0))
            {
                this.isActivated.Value = false;
                this.isUpperLimitOvered.Value = true;
                return;
            }

            this.Tetrimino.Value = null;
            this.placedBlocks.Value = result.Item2;
        }
        public void SpeedUp()
        {
            const int min = 15;
            var interval = this.Timer.Interval / 2;
            this.Timer.Interval = Math.Max(interval, min);
        }
        #endregion


        #region 判定 / その他
        /// <summary>
        /// 衝突判定を行います。
        /// </summary>
        /// <param name="block">チェック対象のブロック</param>
        /// <returns>衝突している場合true</returns>
        private bool CheckCollision(Block block)
        {
            if (block == null)
                throw new ArgumentNullException(nameof(block));

            //--- 左側の壁にめり込んでいる
            if (block.Position.Column < 0)
                return true;

            //--- 右側の壁にめり込んでいる
            if (This.ColumnCount <= block.Position.Column)
                return true;

            //--- 床にめり込んでいる
            if (This.RowCount <= block.Position.Row)
                return true;

            //--- すでに配置済みブロックがある
            return this.placedBlocks.Value.Any(x => x.Position == block.Position);
        }


        /// <summary>
        /// ブロックが揃っていたら消し、配置済みブロックを確定します。
        /// </summary>
        /// <returns>確定された配置済みブロック</returns>
        private Tuple<int, Block[]> RemoveAndFixBlock()
        {
            //--- 行ごとにブロックをまとめる
            var rows = this.placedBlocks.Value
                        .Concat(this.Tetrimino.Value.Blocks)  //--- 配置済みのブロックとテトリミノを合成
                        .GroupBy(x => x.Position.Row)  //--- 行ごとにまとめる
                        .Select(x => new
                        {
                            Row = x.Key,
                            IsFilled = This.ColumnCount <= x.Count(),  //--- 揃っているか
                            Blocks = x,
                        })
                        .ToArray();

            //--- 揃ったブロックを削除して確定
            var blocks = rows
                        .OrderByDescending(x => x.Row)    //--- 深い方から並び替え
                        .WithIndex(x => x.IsFilled)       //--- 揃っている行が見つかるたびにインクリメント
                        .Where(x => !x.Element.IsFilled)  //--- 揃っている行は消す
                        .SelectMany(x =>
                        {
                            //--- ズラす必要がない行はそのまま処理
                            //--- 処理パフォーマンス向上のため特別処理
                            if (x.Index == 0)
                                return x.Element.Blocks;

                            //--- 消えた行のぶん下に段をズラす
                            return x.Element.Blocks.Select(y =>
                            {
                                var position = new Position(y.Position.Row + x.Index, y.Position.Column);
                                return new Block(y.Color, position);
                            });
                        })
                        .ToArray();

            //--- 削除した行数
            var removedRowCount = rows.Count(x => x.IsFilled);
            return Tuple.Create(removedRowCount, blocks);
        }
        #endregion
    }
}
