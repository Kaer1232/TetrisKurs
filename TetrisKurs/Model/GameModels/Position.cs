using This = TetrisKurs.Model.GameModels.Position;

namespace TetrisKurs.Model.GameModels
{
    public class Position
    {
        public int Row { get; }
        public int Column { get; }
        public Position(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }
        public override int GetHashCode() => this.Row.GetHashCode() ^ this.Column.GetHashCode();

        public override string ToString() => $"{this.Row} {this.Column}";

        public static bool operator ==(This left, This right) => left.Row == right.Row && left.Column == right.Column;

        public static bool operator !=(This left, This right) => !(left == right);
    }
}
