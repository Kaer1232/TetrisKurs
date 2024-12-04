namespace TetrisKurs.Base
{
    public struct IndexedItem<T>
    {
        public int Index { get; }

        public T Element { get; }
        internal IndexedItem(int index, T element)
        {
            this.Index = index;
            this.Element = element;
        }
    }

    public struct IndexedItem2<T>
    {
        public int X { get; }

        public int Y { get; }

        public T Element { get; }

        internal IndexedItem2(int x, int y, T element)
        {
            this.X = x;
            this.Y = y;
            this.Element = element;
        }
    }
}