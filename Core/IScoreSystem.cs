using System.Collections.Generic;

namespace Match_3.Core
{
    public interface IScoreSystem
    {
        event System.Action<int> ScoreChanged;
        int Score { get; }
        void AddScore(IEnumerable<Cell> cluster);
    }
}