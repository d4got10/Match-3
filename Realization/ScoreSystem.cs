using Match_3.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Match_3.Realization
{
    public class ScoreSystem : IScoreSystem
    {
        public event Action<int> ScoreChanged;


        public int Score { get; private set; }


        public void AddScore(IEnumerable<Cell> cluster)
        {
            Score += cluster.Count();
            ScoreChanged?.Invoke(Score);
        }
    }
}
