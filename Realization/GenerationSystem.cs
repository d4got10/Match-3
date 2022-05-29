using Match_3.Core;
using Match_3.Core.Gems;
using System;

namespace Match_3.Realization
{
    public class GenerationSystem : IGenerationSystem
    {
        public GenerationSystem(IGemFactory gemFactory)
        {
            GemFactory = gemFactory;
            Random = new Random();
        }


        private IGemFactory GemFactory { get; }
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
                GemType.Blue
            };

            var type = gemTypes[Random.Next() % gemTypes.Length];

            return GemFactory.Create(type);
        }
    }
}
