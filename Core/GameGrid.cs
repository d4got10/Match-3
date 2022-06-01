using Match_3.Core.Utils;

namespace Match_3.Core
{
    public class GameGrid
    {
        public GameGrid(Vector2Int size)
        {
            Size = size;
            _data = new Cell[size.X, size.Y];
            for (int x = 0; x < size.X; x++)
                for (int y = 0; y < size.Y; y++)
                    _data[x, y] = new Cell(new Vector2Int(x, y));
        }


        public Vector2Int Size { get; }
        public Cell this[Vector2Int position] => _data[position.X, position.Y];
        public Cell this[int x, int y] => _data[x, y];


        private Cell[,] _data;


        public bool PositionIsInsidePlayableGrid(Vector2Int position)
        {
            return position.X >= 0 && position.X < Size.X && position.Y >= 0 && position.Y < Size.Y - 1;
        }
    }
}
