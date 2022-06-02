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

        protected IAnimationSystem AnimationSystem { get; }

        protected Cell FirstSwapped { get; private set; }
        protected Cell SecondSwapped { get; private set; }


        public bool Swap(GameGrid grid, Vector2Int first, Vector2Int second)
        {
            FirstSwapped = grid[first];
            SecondSwapped = grid[second];

            var dir = first - second;


            if (Math.Abs(dir.X) + Math.Abs(dir.Y) == 1)
            {
                SwapSuccessfuly(FirstSwapped, SecondSwapped);
                return true;
            }
            else
            {
                SwapFailed(FirstSwapped, SecondSwapped);
                return false;
            }
        }

        public void FailLastSwap(GameGrid grid)
        {
            SwapSuccessfuly(FirstSwapped, SecondSwapped);
        }

        private void SwapSuccessfuly(Cell first, Cell second)
        {
            SwapGems(first, second);

            AnimationSystem.AnimateSuccessfulSwap(first.ContainedGem, second.ContainedGem);
        }

        private void SwapFailed(Cell first, Cell second)
        {
            //AnimationSystem.AnimateFailedSwap(first.ContainedGem, second.ContainedGem);
        }

        private static void SwapGems(Cell first, Cell second)
        {
            var temp = first.ContainedGem;
            first.ContainedGem = second.ContainedGem;
            second.ContainedGem = temp;
        }
    }
}
