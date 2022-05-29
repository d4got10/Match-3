using Match_3.Core.Gems;
using Match_3.Core.Utils;

namespace Match_3.Core
{
    public class Cell
    {
        public Cell(Vector2Int position)
        {
            Position = position;
        }

        public Vector2Int Position { get; }
        public Gem ContainedGem { get; set; }
    }
}
