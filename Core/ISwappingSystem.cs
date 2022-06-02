using Match_3.Core.Utils;

namespace Match_3.Core
{
    public interface ISwappingSystem
    {
        bool Swap(GameGrid grid, Vector2Int first, Vector2Int second);
        void FailLastSwap(GameGrid grid);
    }
}