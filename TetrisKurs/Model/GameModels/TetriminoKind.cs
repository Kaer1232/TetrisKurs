using System;
using System.Collections.Generic;
using System.Linq;



namespace TetrisKurs.Model.GameModels 
{
    public enum TetriminoKind
    {
        I = 0,
        O,
        S,
        Z,
        J,
        L,
        T,
    }

    public static class TetriminoExtensions
    {
        public static Color BlockColor(this TetriminoKind self)
        {
            switch (self)
            {
                case TetriminoKind.I:   return Colors.LightBlue;
                case TetriminoKind.O:   return Colors.Yellow;
                case TetriminoKind.S:   return Colors.YellowGreen;
                case TetriminoKind.Z:   return Colors.Red;
                case TetriminoKind.J:   return Colors.Blue;
                case TetriminoKind.L:   return Colors.Orange;
                case TetriminoKind.T:   return Colors.Purple;
            }
            throw new InvalidOperationException("Unknown Tetrimino");
        }

        public static Position InitialPosition(this TetriminoKind self)
        {
            int length = 0;
            switch (self)
            {
                case TetriminoKind.I:   length = 4; break;
                case TetriminoKind.O:   length = 2; break;
                case TetriminoKind.S:
                case TetriminoKind.Z:
                case TetriminoKind.J:
                case TetriminoKind.L:
                case TetriminoKind.T:   length = 3; break;
                default:    throw new InvalidOperationException("Unknown Tetrimino");
            }

            var row = -length;
            var column = (Field.ColumnCount - length) / 2;
            return new Position(row, column);
        }
        public static IReadOnlyList<Block> CreateBlock(this TetriminoKind self, Position offset, Direction direction = Direction.Up)
        {
            int[,] pattern = null;
            switch (self)
            {
                #region I
                case TetriminoKind.I:
                    switch (direction)
                    {
                        case Direction.Up:
                            pattern = new int[,]
                            {
                                { 0, 1, 0, 0 },
                                { 0, 1, 0, 0 },
                                { 0, 1, 0, 0 },
                                { 0, 1, 0, 0 },
                            };
                            break;

                        case Direction.Right:
                            pattern = new int[,]
                            {
                                { 0, 0, 0, 0 },
                                { 1, 1, 1, 1 },
                                { 0, 0, 0, 0 },
                                { 0, 0, 0, 0 },
                            };
                            break;

                        case Direction.Down:
                            pattern = new int[,]
                            {
                                { 0, 0, 1, 0 },
                                { 0, 0, 1, 0 },
                                { 0, 0, 1, 0 },
                                { 0, 0, 1, 0 },
                            };
                            break;

                        case Direction.Left:
                            pattern = new int[,]
                            {
                                { 0, 0, 0, 0 },
                                { 0, 0, 0, 0 },
                                { 1, 1, 1, 1 },
                                { 0, 0, 0, 0 },
                            };
                            break;
                    }
                    break;
                #endregion

                #region O
                case TetriminoKind.O:
                    pattern = new int[,]
                    {
                        { 1, 1 },
                        { 1, 1 },
                    };
                    break;
                #endregion

                #region S
                case TetriminoKind.S:
                    switch (direction)
                    {
                        case Direction.Up:
                            pattern = new int[,]
                            {
                                { 0, 1, 1 },
                                { 1, 1, 0 },
                                { 0, 0, 0 },
                            };
                            break;

                        case Direction.Right:
                            pattern = new int[,]
                            {
                                { 0, 1, 0 },
                                { 0, 1, 1 },
                                { 0, 0, 1 },
                            };
                            break;

                        case Direction.Down:
                            pattern = new int[,]
                            {
                                { 0, 0, 0 },
                                { 0, 1, 1 },
                                { 1, 1, 0 },
                            };
                            break;

                        case Direction.Left:
                            pattern = new int[,]
                            {
                                { 1, 0, 0 },
                                { 1, 1, 0 },
                                { 0, 1, 0 },
                            };
                            break;
                    }
                    break;
                #endregion

                #region Z
                case TetriminoKind.Z:
                    switch (direction)
                    {
                        case Direction.Up:
                            pattern = new int[,]
                            {
                                { 1, 1, 0 },
                                { 0, 1, 1 },
                                { 0, 0, 0 },
                            };
                            break;

                        case Direction.Right:
                            pattern = new int[,]
                            {
                                { 0, 0, 1 },
                                { 0, 1, 1 },
                                { 0, 1, 0 },
                            };
                            break;

                        case Direction.Down:
                            pattern = new int[,]
                            {
                                { 0, 0, 0 },
                                { 1, 1, 0 },
                                { 0, 1, 1 },
                            };
                            break;

                        case Direction.Left:
                            pattern = new int[,]
                            {
                                { 0, 1, 0 },
                                { 1, 1, 0 },
                                { 1, 0, 0 },
                            };
                            break;
                    }
                    break;
                #endregion

                #region J
                case TetriminoKind.J:
                    switch (direction)
                    {
                        case Direction.Up:
                            pattern = new int[,]
                            {
                                { 1, 0, 0 },
                                { 1, 1, 1 },
                                { 0, 0, 0 },
                            };
                            break;

                        case Direction.Right:
                            pattern = new int[,]
                            {
                                { 0, 1, 1 },
                                { 0, 1, 0 },
                                { 0, 1, 0 },
                            };
                            break;

                        case Direction.Down:
                            pattern = new int[,]
                            {
                                { 0, 0, 0 },
                                { 1, 1, 1 },
                                { 0, 0, 1 },
                            };
                            break;

                        case Direction.Left:
                            pattern = new int[,]
                            {
                                { 0, 1, 0 },
                                { 0, 1, 0 },
                                { 1, 1, 0 },
                            };
                            break;
                    }
                    break;
                #endregion

                #region L
                case TetriminoKind.L:
                    switch (direction)
                    {
                        case Direction.Up:
                            pattern = new int[,]
                            {
                                { 0, 0, 1 },
                                { 1, 1, 1 },
                                { 0, 0, 0 },
                            };
                            break;

                        case Direction.Right:
                            pattern = new int[,]
                            {
                                { 0, 1, 0 },
                                { 0, 1, 0 },
                                { 0, 1, 1 },
                            };
                            break;

                        case Direction.Down:
                            pattern = new int[,]
                            {
                                { 0, 0, 0 },
                                { 1, 1, 1 },
                                { 1, 0, 0 },
                            };
                            break;

                        case Direction.Left:
                            pattern = new int[,]
                            {
                                { 1, 1, 0 },
                                { 0, 1, 0 },
                                { 0, 1, 0 },
                            };
                            break;
                    }
                    break;
                #endregion

                #region T
                case TetriminoKind.T:
                    switch (direction)
                    {
                        case Direction.Up:
                            pattern = new int[,]
                            {
                                { 0, 1, 0 },
                                { 1, 1, 1 },
                                { 0, 0, 0 },
                            };
                            break;

                        case Direction.Right:
                            pattern = new int[,]
                            {
                                { 0, 1, 0 },
                                { 0, 1, 1 },
                                { 0, 1, 0 },
                            };
                            break;

                        case Direction.Down:
                            pattern = new int[,]
                            {
                                { 0, 0, 0 },
                                { 1, 1, 1 },
                                { 0, 1, 0 },
                            };
                            break;

                        case Direction.Left:
                            pattern = new int[,]
                            {
                                { 0, 1, 0 },
                                { 1, 1, 0 },
                                { 0, 1, 0 },
                            };
                            break;
                    }
                    break;
                #endregion
            }

            if (pattern == null)
                throw new InvalidOperationException("Unknown Tetrimino");

            var color = self.BlockColor();
            return  Enumerable.Range(0, pattern.GetLength(0))
                    .SelectMany(r => Enumerable.Range(0, pattern.GetLength(1)).Select(c => new Position(r, c)))
                    .Where(x => pattern[x.Row, x.Column] != 0)
                    .Select(x => new Position(x.Row + offset.Row, x.Column + offset.Column))
                    .Select(x => new Block(color, x))
                    .ToArray();
        }
    }
}