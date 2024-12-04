namespace TetrisKurs.Model.GameModels
{
    public class Block
    {
        public Color Color { get; set; }
        public Position Position { get; }
        public Block(Color color, Position position)
        {
            this.Color = color;
            this.Position = position;
        }
    }
}
