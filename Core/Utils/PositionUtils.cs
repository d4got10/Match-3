using Match_3.Core.Utils;
using Match_3.Realization;

namespace Match_3.Utils
{
    public static class PositionUtils
    {
        public static Vector2 ConvertGridToWorldPosition(Vector2Int position, Settings settings)
        {
            return position * settings.CellSize;
        }

        public static Vector2Int ConvertWorldToGridPosition(Vector2 position, Settings settings)
        {
            int x = (int)(position.X / settings.CellSize);
            int y = (int)(position.Y / settings.CellSize);
            return new Vector2Int(x, y);
        }
    }
}
