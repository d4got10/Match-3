using Match_3.Core;
using Match_3.Core.Utils;

namespace Match_3.Realization
{
    public class SheddingSystem : ISheddingSystem
    {
        public SheddingSystem(IAnimationSystem animationSystem)
        {
            AnimationSystem = animationSystem;
        }


        private IAnimationSystem AnimationSystem { get; }


        public void Move(GameGrid grid)
        {
            for(int y = 0; y < grid.Size.Y - 1; y++)
            {
                for(int x = 0; x < grid.Size.X; x++)
                {
                    if (grid[x, y].IsEmpty == false) continue;

                    if (grid[x, y + 1].IsEmpty) continue;

                    var gem = grid[x, y + 1].ContainedGem;
                    grid[x, y].ContainedGem = gem;
                    grid[x, y + 1].ContainedGem = null;
                    AnimationSystem.AnimateMovement(gem, new Vector2Int(x, y + 1), new Vector2Int(x, y));
                }
            }
        }
    }
}
