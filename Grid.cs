using Match_3.Core.Utils;

namespace Match_3.Core
{
    public class Grid
    {
        public Grid(Vector2Int size)
        {
            _data = new Cell[size.X, size.Y];
        }

        private Cell[,] _data;
    }
}
