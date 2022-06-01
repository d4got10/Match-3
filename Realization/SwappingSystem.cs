using Match_3.Core;
using Match_3.Core.Utils;
using System;

namespace Match_3.Realization
{
    public class SwappingSystem : ISwappingSystem
    {
        public SwappingSystem(IAnimationSystem animationSystem)
        {
            AnimationSystem = animationSystem;
        }


        public Cell Selected { get; private set; }

        protected IAnimationSystem AnimationSystem { get; }


        public bool Swap(GameGrid grid, Vector2Int first, Vector2Int second)
        {
            var firstCell = grid[first];
            var secondCell = grid[second];

            var dir = first - second;


            if (Math.Abs(dir.X) + Math.Abs(dir.Y) == 1)
            {
                SwapSuccessfuly(firstCell, secondCell);
                return true;
            }
            else
            {
                SwapFailed(firstCell, secondCell);
                return false;
            }
        }

        private void SwapSuccessfuly(Cell first, Cell second)
        {
            SwapGems(first, second);

            AnimationSystem.AnimateSuccessfulSwap(first.ContainedGem, second.ContainedGem);
        }

        private void SwapFailed(Cell first, Cell second)
        {
            AnimationSystem.AnimateFailedSwap(first.ContainedGem, second.ContainedGem);
        }

        private static void SwapGems(Cell first, Cell second)
        {
            var temp = first.ContainedGem;
            first.ContainedGem = second.ContainedGem;
            second.ContainedGem = temp;
        }
    }
}
