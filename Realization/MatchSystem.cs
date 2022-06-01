using Match_3.Core;
using Match_3.Core.Gems;
using Match_3.Core.Utils;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Match_3.Realization
{
    public class MatchSystem : IMatchSystem
    {
        public MatchSystem(IAnimationSystem animationSystem)
        {
            AnimationSystem = animationSystem;
        }

        protected IAnimationSystem AnimationSystem { get; }


        public bool DestroyMatches(GameGrid grid)
        {
            bool cleared = false;

            var verticalMatches = GetVerticalMatches(grid);
            var horizontalMatches = GetHorizontalMatches(grid);

            cleared |= verticalMatches.Count > 0;
            cleared |= horizontalMatches.Count > 0;

            DestroyMatches(verticalMatches);
            DestroyMatches(horizontalMatches);

            return cleared;
        }

        private void DestroyMatches(List<HashSet<Cell>> matches)
        {
            foreach (var cluster in matches)
            {
                foreach (var cell in cluster)
                {
                    if (cell.IsEmpty == false)
                        DestroyGemInCell(cell);
                }
            }
        }

        private void DestroyGemInCell(Cell cell)
        {
            AnimationSystem.AnimateDestruction(cell.ContainedGem);
            cell.ContainedGem = null;
        }

        private List<HashSet<Cell>> GetVerticalMatches(GameGrid grid)
        {
            var matches = new List<HashSet<Cell>>();

            HashSet<Cell> markedCells = new HashSet<Cell>();
            for (int y = 0; y < grid.Size.Y - 1; y++)
            {
                for (int x = 0; x < grid.Size.X; x++)
                {
                    var cell = grid[x, y];
                    if (markedCells.Contains(cell)) continue;

                    var cluster = new HashSet<Cell>();
                    var visited = new HashSet<Cell>();
                    GetClusterRecursive(grid, cluster, visited, new Vector2Int(x, y), grid[x, y].ContainedGem.Type, new Vector2Int(0, 1));

                    foreach (var cellInCluster in cluster)
                        markedCells.Add(cellInCluster);

                    if (cluster.Count > 2)
                    {
                        matches.Add(cluster);
                    }
                }
            }

            return matches;
        }

        private List<HashSet<Cell>> GetHorizontalMatches(GameGrid grid)
        {
            var matches = new List<HashSet<Cell>>();

            HashSet<Cell> markedCells = new HashSet<Cell>();
            for (int x = 0; x < grid.Size.X; x++)
            {
                for (int y = 0; y < grid.Size.Y - 1; y++)
                {
                    var cell = grid[x, y];
                    if (markedCells.Contains(cell)) continue;

                    var cluster = new HashSet<Cell>();
                    var visited = new HashSet<Cell>();
                    GetClusterRecursive(grid, cluster, visited, new Vector2Int(x, y), grid[x, y].ContainedGem.Type, new Vector2Int(1, 0));

                    foreach (var cellInCluster in cluster)
                        markedCells.Add(cellInCluster);

                    if (cluster.Count > 2)
                    {
                        matches.Add(cluster);
                    }
                }
            }

            return matches;
        }

        private void GetClusterRecursive(GameGrid grid, HashSet<Cell> cluster, HashSet<Cell> visited, Vector2Int position, GemType type, Vector2Int direction)
        {
            var cell = grid[position];
            cluster.Add(cell);

            var newPosition = position + direction;
            if (grid.PositionIsInsidePlayableGrid(newPosition) == false) return;

            var newCell = grid[newPosition];
            if (visited.Contains(newCell)) return;

            visited.Add(newCell);

            if (newCell.IsEmpty) return;

            if (newCell.ContainedGem.Type != type) return;

            GetClusterRecursive(grid, cluster, visited, newPosition, type, direction);
        }
    }
}
