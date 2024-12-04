using TetrisKurs.Utilities;
using This = TetrisKurs.Model.GameModels.Tetrimino;



namespace TetrisKurs.Model.GameModels
{
    public class Tetrimino
    {
        public TetriminoKind Kind { get; }

        public Color Color => this.Kind.BlockColor();

        public Position Position { get; private set; }

        public Direction Direction { get; private set; }

        public IReadOnlyList<Block> Blocks { get; private set; }

        private Tetrimino(TetriminoKind kind)
        {
            this.Kind = kind;
            this.Position = kind.InitialPosition();
            this.Blocks = kind.CreateBlock(this.Position);
        }

        public static TetriminoKind RandomKind()
        {
            var length = Enum.GetValues(typeof(TetriminoKind)).Length;
            return (TetriminoKind)RandomProvider.ThreadRandom.Next(length);
        }

        public static This Create(TetriminoKind? kind = null)
        {
            kind = kind ?? This.RandomKind();
            return new This(kind.Value);
        }

        public bool Move(MoveDirection direction, Func<Block, bool> checkCollision)
        {
            var position = this.Position;
            if (direction == MoveDirection.Down)
            {
                var row = position.Row + 1;
                position = new Position(row, position.Column);
            }
            else
            {
                var delta = (direction == MoveDirection.Right) ? 1 : -1;
                var column = position.Column + delta;
                position = new Position(position.Row, column);
            }

            var blocks = this.Kind.CreateBlock(position, this.Direction);

            if (blocks.Any(checkCollision))
                return false;

            this.Position = position;
            this.Blocks = blocks;
            return true;
        }

        public bool Rotation(RotationDirection rotationDirection, Func<Block, bool> checkCollision)
        {
            var count = Enum.GetValues(typeof(Direction)).Length;
            var delta = (rotationDirection == RotationDirection.Right) ? 1 : -1;
            var direction = (int)this.Direction + delta;
            if (direction < 0)      direction += count;
            if (direction >= count) direction %= count;

            var adjustPattern   = this.Kind == TetriminoKind.I
                                ? new [] { 0, 1, -1, 2, -2 } 
                                : new [] { 0, 1, -1 }; 
            foreach (var adjust in adjustPattern)
            {
                var position = new Position(this.Position.Row, this.Position.Column + adjust);
                var blocks = this.Kind.CreateBlock(position, (Direction)direction);

                if (!blocks.Any(checkCollision))
                {
                    this.Direction = (Direction)direction;
                    this.Position = position;
                    this.Blocks = blocks;
                    return true;
                }
            }
            return false;
        }
    }
}