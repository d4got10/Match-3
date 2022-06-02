namespace Match_3.Core
{
    public struct GameCoreDependecies
    {
        public ISheddingSystem SheddingSystem { get; set; }
        public IMatchSystem MatchSystem { get; set; }
        public IGenerationSystem GenerationSystem { get; set; }
        public IAnimationSystem AnimationSystem { get; set; }
        public ISelectionSystem SelectionSystem { get; set; }
        public ISwappingSystem SwappingSystem { get; set; }
        public IScoreSystem ScoreSystem { get; set; }
    }
}