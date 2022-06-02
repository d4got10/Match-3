using Match_3.Core;
using Match_3.Core.Gems;
using Match_3.Core.Utils;
using System;

namespace Match_3.Realization
{
    public class GenerationSystem : IGenerationSystem
    {
        public GenerationSystem(IGemFactory gemFactory, IAnimationSystem animationSystem)
        {
            GemFactory = gemFactory;
            AnimationSystem = animationSystem;
            Random = new Random();
        }


        private IGemFactory GemFactory { get; }
        private IAnimationSystem AnimationSystem { get; }
        private Random Random { get; }


        public void Generate(GameGrid grid)
        {
            Cell cell;
            for (int x = 0; x < grid.Size.X; x++)
            {
                cell = grid[x, grid.Size.Y - 1];
                if (cell.IsEmpty == false) continue;

                var gem = CreateGem();
                cell.ContainedGem = gem;
                AnimationSystem.AnimateAppearence(gem, new Vector2Int(x, grid.Size.Y - 1));
            }
        }

        private Gem CreateGem()
        {
            var gemTypes = new GemType[]
            {
                GemType.Red,
                GemType.Blue,
                GemType.Yellow,
                GemType.Green,
                GemType.Violet
            };

            var type = gemTypes[Random.Next() % gemTypes.Length];

            return GemFactory.Create(type);
        }
    }
}
